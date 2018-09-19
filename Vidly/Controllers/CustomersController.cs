using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
//use System.Data.Entity for eager loading (2 @model types at once)
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        //To access a database we need a private DbContext
        private ApplicationDbContext _context;

        //the DbContext must also be initialized in the constructor
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //The above DbContext is a disposable object so we must properly dispose it.
        //to do that we must override the Dispose method
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            //if we return customer MVC will look for a view named Edit (which we dont want to create)
            //in order to redirect it we use "New", to tell it to New instead
            return View("CustomerForm", viewModel);
        }

        //if your ActionResults modify any data they should never be accesible by HttpGet, so use HttpPost annotation
        [HttpPost]

        //ensure our form cannot be CSRF attacked by hackers
        [ValidateAntiForgeryToken]

        //passing an argument in an ActionResult IE: (NewCustomerViewModel viewModel) is called model binding.
        //Model Binding maps data from an Http request to action method parameters by searching forms, routes, and query strings.
        //In order for model binding to work the contructor and properties must be public/writeable.
        public ActionResult Save(Customer customer)
        {
            //we can use the ModelState property to get access to validation data
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                //if the ModelState is invalid you always want to return the same view
                return View("CustomerForm", viewModel);
            }
            //If our customer has no Id, we want to add them to our database          
            if(customer.Id == 0)
            {
                //this only adds customer to the memory and does not put it in the database
                //whne we add anything to a database, all changes must be valid and accepted or none will be accepted
                _context.Customers.Add(customer);
            }
            else
            {
                //to edit an entity, we need to call that entity first, then edit its properties
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                //We could also use an AutoMapper to code this using something like Mapper.Map(customer, customerInDb)
                //Mapper.Map(customer, customerInDb) The second argument tries to copy the attributes from the first argument
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }

            //To save our customer to the database we use:
            _context.SaveChanges();

            //lastly we need to redirect this added customer back to our list of customers.
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult CustomerForm()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View(viewModel);
        }

        // GET: Customers
        public ActionResult Index()
        {
            //Code that was used to get a hard-coded list of customers
            //var customers = GetCustomers();   

            //This Customers property is a DBSet we defined in our DbContext, with it we can get all customers in the database
            //when the below line is executed entity framework is not going to query the database, the query is created when we iterate over this object.
            //if we want to immediately query our database we can use a method like ToList()
            //We use the Include method when trying to use eager loading. 
            //This will allow us to access the MembershipType which is stored in a different model from the one we load in the index.cshtml
            //We must also add using System.Data.Entity; above for this to work.
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            var viewModel = new CustomerListViewModel { Customers = customers };

            return View(viewModel);
        }
        
        public ActionResult Details(int id)
        {
            ////Mosh's code, cleaner then mine.
            ////SingleOrDefault returns 1 element from a list the lambda expression evaluates if the element matches the id
            //var customers = GetCustomers().SingleOrDefault(c => c.Id == id);
            //if (customers == null)
            //    return HttpNotFound();

            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            foreach (var customer in customers)
            {
                if (customer.Id == id)
                {
                    var viewModel = customer;
                    return View(viewModel);
                }
            }
            return HttpNotFound();

        }

        //private List<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer { Id = 1, Name = "Doug Clarke" },
        //        new Customer { Id = 2, Name = "Samantha Giroux" },
        //        new Customer { Id = 3, Name = "Adam Smith" }
        //    };
        //}
    }
}