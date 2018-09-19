﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/customers
        //we want to return a list of customers so we use IEnumerable
        //this is also how we map our Customer object to the CustomerDto
        public IHttpActionResult GetCustomers()
        {
            //.Select is a LINQ method
            //return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
            var customerDtos = _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        //GET /api/customers/1
        //We are returning a CustomerDto not a customer anymore, so we change the return type to CustomerDto
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                //if the customer is not found we return the standard Http Not Found response
                return NotFound();

            //We can't use .Select since we are only returning 1 customer
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST api/customers
        //we are creating a resource so we need use HttpPost
        [HttpPost]
        //The below method will not return the proper success code 201
        //the customer object will be in the request body and ASP.NET will automatically initialize this
        //public CustomerDto CreateCustomer(CustomerDto customerDto)
        //{
        //    if (!ModelState.IsValid)
        //        //if the state is not valid we send a bad request exception
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    //we need to convert our CustomerDto to a Customer object to save it to the database
        //    var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
        //    _context.Customers.Add(customer);
        //    _context.SaveChanges();

        //    //this customer object has an id that is generated by the database so we want to add this Id to the dto and return it to the client
        //    customerDto.Id = customer.Id;
        //    return customerDto;
        //}

        //Instead of CustomerDto we use IHttpActionResult
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                //if the state is not valid we send a bad request exception
                //BadRequest() is an IHttActionResult helper method
                return BadRequest();

            //we need to convert our CustomerDto to a Customer object to save it to the database
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            //this customer object has an id that is generated by the database after it is created and saved.
            //we need to pass this Id to the Dto and return it to the client.
            customerDto.Id = customer.Id;

            //Instead of returning customerDto we call the Created() method
            //The created method takes 2 arguments, a URI and the actual object that was created
                //we need to return the uri of the newly created resource to the client
                    //Our uri would look something like: /api/customers/10
                    //the /10 would be the newly created Id from the database
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                //if the stat is not valid we send a bad request exception
                return BadRequest();

            //getting the customer we want to update from the passed id
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            //we have to check to make sure the id passed is valid
            if (customerInDb == null)
                //if the customer is not found we return the standard Http Not Found response
                return NotFound();

            //if you have an existing object you can pass that here as a second argument.
            //the reason we are passing customerInDb is because this object is loaded into our _context 
            //and we want to be able to pass changes in this object
            Mapper.Map(customerDto, customerInDb);          
            _context.SaveChanges();
            return Ok();
        }

        //DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                //if the customer is not found we return the standard Http Not Found response
                return NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
            return Ok();
        }

    }
}