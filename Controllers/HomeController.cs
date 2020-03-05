using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Products_and_categories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Products_and_categories.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext=context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("products")]
        public IActionResult Products()
        {
            ViewBag.AllProducts = dbContext.Products;
            return View("products");
        }


        [HttpPost("add/product")]
        public IActionResult AddProduct(Product newProduct)
        {
            if(ModelState.IsValid)
            {
                dbContext.Products.Add(newProduct);
                dbContext.SaveChanges();
                return RedirectToAction("Products");
            }
            else
            {
                ViewBag.AllProducts =dbContext.Products;
                return View("Products");
            }
        }


        [HttpGet("products/{productId}")]
        public IActionResult ShowProduct(int productId)
        {
            var thisProduct = dbContext.Products
                .Include(product => product.Associations)
                .ThenInclude(a => a.Category)
                .FirstOrDefault(a => a.ProductId == productId);
            var notinCategory = dbContext.Categories
                .Include(category => category.Associations)
                .Where(category => category.Associations.All(product => product.ProductId != productId));
            ViewBag.notinCategory = notinCategory;    
            
            return View(thisProduct);
        }

        [HttpPost("addtoproduct")]
        public IActionResult AddToProduct(Association newAssociation)
        {
            dbContext.Associations.Add(newAssociation);
            dbContext.SaveChanges();    
            return RedirectToAction("ShowProduct");      
        }


        [HttpGet("categories")]
        public IActionResult Categories()
        {
            ViewBag.AllCategories = dbContext.Categories;
            return View("categories");
        }

        
        [HttpPost("add/category")]
        public IActionResult AddCategory(Category newCategory)
        {
            if(ModelState.IsValid)
            {
                dbContext.Categories.Add(newCategory);
                dbContext.SaveChanges();
                return RedirectToAction("Categories");
            }
            else
            {
                ViewBag.AllCategories=dbContext.Categories;
                return View("Categories");
            }
        }

        [HttpGet("categories/{categoryId}")]
        public IActionResult ShowCategory(int categoryId)
        {
            var thisCategory = dbContext.Categories
                .Include(category => category.Associations)
                .ThenInclude(a =>a.Product)
                .FirstOrDefault(a => a.CategoryId == categoryId);
            ViewBag.Category =thisCategory;
            ViewBag.Associations = dbContext.Associations;
            var notinproduct = dbContext.Products
                .Include(product => product.Associations)
                .Where(product => product.Associations.All(category => category.CategoryId != categoryId));
            ViewBag.notinproduct = notinproduct;    
            
            return View(thisCategory);
        }


        

        [HttpPost("addtocategory")]
        public IActionResult AddToCategory(Association newAssociation)
        {
            dbContext.Associations.Add(newAssociation);
            dbContext.SaveChanges();    
            return RedirectToAction("ShowCategory");      
        }

    }
}
