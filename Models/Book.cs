using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Models
{
    public class Book
    {
        [key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        public String Auther { get; set; }
        [Range(0.01, 100.00, ErrorMessage ="Price must be between 0 and 100")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }   

    }
}
