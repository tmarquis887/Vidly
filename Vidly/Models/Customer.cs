using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        //import System.ComponentModel.DataAnnotations
        //these annotaions affect the way this data is stored in the database
        //required makes the type not null
        //StringLength sets the maximum number of characters for a string entry
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        //applying our custom validation annotation
        [Min18YearsIfAMember]
        [Display(Name = "Date of Birth")]
        public DateTime? BirthDate { get; set; }

        
        //this is called a navigation property, it allows us to navigate from customer to its membership type
        public MembershipType MembershipType { get; set; }

        //you may not want to load the entire membership object, and may only want the foreign key.
        //Entity Framework recognizes this convention and treats this property as a foreign key
        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }
    }
}