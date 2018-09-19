using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        //To make a DTO copy all the properties from the customer model to the DTO
        //We want either primative types or custom DTOs

        //We don't want any properties that rely on another object (dependence) in our domain so we don't use the below property
        //public MembershipType MembershipType { get; set; }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        //[Display(Name = "Date of Birth")] - These are unneccessary in our DTO
        //[Min18YearsIfAMember] must be temporarily commented out
        public DateTime? BirthDate { get; set; }

        public byte MembershipTypeId { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

        //We can now map our DTO to any API method
    }
}