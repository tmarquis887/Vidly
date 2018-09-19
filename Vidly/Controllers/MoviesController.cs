using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
//use System.Data.Entity for eager loading (2 @model types at once)
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        //To access a database we need a private DbContext
        private ApplicationDbContext _context;

        //the DbContext must also be initialized in the constructor
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //The above DbContext is a disposable object so we must properly dispose it.
        //to do that we must override the Dispose method
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            var movies = _context.Movie.Include(m => m.GenreType).ToList();

            var viewModel = new MovieListViewModel { Movies = movies };

            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movie.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {             
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult MovieForm()
        {
            var genreTypes = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genreTypes
            };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                //if the ModelState is invalid you always want to return the same view
                return View("MovieForm", viewModel);
            }
            //If our customer has no Id, we want to add them to our database          
            if (movie.Id == 0)
            {
                //when we add anything to a database, all changes must be valid and accepted or none will be accepted
                //this only adds customer to the memory and does not put it in the database
                movie.DateAdded = DateTime.Now;
                var genre = _context.Genres.FirstOrDefault(g => g.Id == movie.GenreId);
                movie.GenreType = genre;
                _context.Movie.Add(movie);
            }
            else
            {
                //to edit an entity, we need to call that entity first, then edit its properties
                var customerInDb = _context.Movie.Single(m => m.Id == movie.Id);
                var genre = _context.Genres.FirstOrDefault(m => m.Id == movie.GenreId);
                //We could also use an AutoMapper to code this using something like Mapper.Map(customer, customerInDb)
                //Mapper.Map(customer, customerInDb) The second argument tries to copy the attributes from the first argument
                customerInDb.Name = movie.Name;
                customerInDb.ReleaseDate = movie.ReleaseDate;
                customerInDb.GenreType = genre;
                customerInDb.GenreId = (byte)genre.Id;
                customerInDb.NumberInStock = movie.NumberInStock;
            }
            //To save our customer to the database we use:
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }

            //lastly we need to redirect this added customer back to our list of customers in index.
            return RedirectToAction("Index", "Movies");
        }
    

        public ActionResult Details(int id)
        {
            var movies = _context.Movie.Include(m => m.GenreType).ToList();
            foreach (var movie in movies)
            {
                if (movie.Id == id)
                {
                    var viewModel = movie;
                    return View(viewModel);
                }
            }
            return HttpNotFound();
        }


        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!"};
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "John Doe" },
                new Customer { Id = 2, Name = "Jane Doe" },
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
            //return new ViewResult() - same as above code

            //Testing other return types:
            //return Content("Hello World");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("About", "Home", new { page = 1, sortBy = "name"});
        }

        //private List<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie { Id = 1, Name = "Shrek" },
        //        new Movie { Id = 2, Name = "Wall-E" },
        //    };
        //}



        //[Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content(year + "/" + month);
        //}

        //Testing making other ActionResults
        //public ActionResult Edit(int id)
        //{
        //    return Content("id=" + id);
        //}

        //Testing ActionResults that accept arguments
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;

        //    if (String.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";

        //    return Content($"pageIndex={pageIndex}&sortBy={sortBy}");
        //}
    }
}