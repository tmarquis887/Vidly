using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string GenreType { get; set; }
    }
}