using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    //Here we are creating a validation class, to do this we are deriving from the ValidationAttribute class
    public class Min18YearsIfAMember : ValidationAttribute
    {
        //type override IsValid and choose the second overload to display the code below.
        //we choose the second overload because this gives access to other properties of our model
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //ObjectInstance gives us access to the containing class. 
            //Because we want to use this in the customer class we need to explicitly cast this to the Customer class  
            var customer = (Customer)validationContext.ObjectInstance;

            //we don't want to use numbers like 1 or 0 when using validation, instead make variables with these values in the MembershipType class
            //if (customer.MembershipTypeId == 0 || customer.MembershipTypeId == 1)
            if (customer.MembershipTypeId == MembershipType.Unknown || 
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
                //Unknown refers 0 in our MembershipTypeId which does not need to be validated, the code below returns a successful validation
                //PayAsYouGo refers 1 in our MembershipTypeId which does not need to be validated, the code below returns a successful validation
                return ValidationResult.Success;

            //if no birthdate is entered, but a subscription other then option 1 is selected this is triggered
            if (customer.BirthDate == null)
                return new ValidationResult("Birthdate is required.");

            //this calculates our customers age for our subscription validation
            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;

            //determines what to do based on our customers age
            return (age >= 18)
                ? ValidationResult.Success 
                : new ValidationResult("You must be at least 18 to have an active subscription.");
        }
    }
}