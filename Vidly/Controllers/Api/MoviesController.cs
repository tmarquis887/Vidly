using AutoMapper;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET api/movies
        [HttpGet]
        public IHttpActionResult GetMovies()
        {
            var movieDtos = _context.Movie
                .Include(m => m.GenreType)
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movieDtos);
        }

        //GET api/movies/1
        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movie.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //POST api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            var genre = _context.Genres.FirstOrDefault(g => g.Id == movie.GenreId);
            movie.GenreType = genre;

            _context.Movie.Add(movie);
            _context.SaveChanges();

            //the movie object is given an id by the database after it is created/saved, we need to pass this Id to the Dto and return it to the client.
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        //PUT api/movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _context.Movie.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDb);

            var genre = _context.Genres.FirstOrDefault(g => g.Id == movieInDb.GenreId);
            movieInDb.GenreType = genre;
           
            _context.SaveChanges();
            return Ok();
        }

        //DELETE api/movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id, MovieDto movieDto)
        {        
            var movieInDb = _context.Movie.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            _context.Movie.Remove(movieInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
