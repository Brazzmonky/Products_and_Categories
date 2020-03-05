using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;

namespace Products_and_categories
{    
    [Table("categories")]
    public class Category
    {
        [Key]
        public int CategoryId{get;set;}

        [Required]
        [MinLength(2,ErrorMessage="Required Field, please input a Category name of at least 2 characters")]
        public string Name { get; set; }
        public List<Association> Associations { get; set; }
        public DateTime created_at=DateTime.Now;
        public DateTime updated_at=DateTime.Now;
    }

}    