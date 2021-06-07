using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
//using SQLite;

namespace ProductCrud.Models
{
    public class Product
    {
        //[PrimaryKey, AutoIncrement]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        public string Photo { get; set; }
    }
}
