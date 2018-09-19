using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        //difference between a list and IEnumerable
        //list gives us access to all the features of list like add, remove, etc... If we replace the list with another collection we have to come back and modify the code
        //IEnumerable only lets us interate over the specified type. If that is all you want to do then IEnumerable is better.
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }
}