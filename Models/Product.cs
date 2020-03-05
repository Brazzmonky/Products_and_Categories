using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;

namespace Products_and_categories
{
    [Table("products")]
    public class Product
    {
        [Key]
        public int ProductId{get;set;}

        [Required]
        [MinLength(3,ErrorMessage="Please input a product name of at least 3 characters!")]
        public string Name{get;set;}

        [Required]
        [MaxLength(100,ErrorMessage="Required Field. Please Input a description of no more then 100 characters.")]
        public string Description{get;set;}

        [Required]
        [Range(0,Int32.MaxValue,ErrorMessage="Required field, please input a price of a positive number")]
        public decimal Price{get;set;}


        public List<Association> Associations{get;set;}


        public DateTime created_at=DateTime.Now;
        public DateTime updated_at=DateTime.Now;
    }
}    