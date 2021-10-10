﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new ProductShopContext();
            db.Database.EnsureDeleted();
            Console.WriteLine("Database was successfully deleted!");
            db.Database.EnsureCreated();
            Console.WriteLine("Database was successfully created!");
            
            string inputJsonUsers= File.ReadAllText("..\\..\\..\\Datasets\\users.json");
            string inputJsonProducts = File.ReadAllText("..\\..\\..\\Datasets\\products.json");
            string inputJsonCategories = File.ReadAllText("..\\..\\..\\Datasets\\categories.json");
            string inputJsonCategoriesProducts = File.ReadAllText("..\\..\\..\\Datasets\\categories-products.json");

            //task 1
            Console.WriteLine(ImportUsers(db, inputJsonUsers));
            //task 2
            Console.WriteLine(ImportProducts(db, inputJsonProducts));
            //task 3
            Console.WriteLine(ImportCategories(db, inputJsonCategories));
            //task 4
            Console.WriteLine(ImportCategoryProducts(db, inputJsonCategoriesProducts));


        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            List<CategoryProduct> mappingCategoryProduct = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);
            context.CategoryProducts.AddRange(mappingCategoryProduct);
            context.SaveChanges();
            return $"Successfully imported {mappingCategoryProduct.Count()}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(inputJson).Where(x=>x.Name!=null).ToList();
            context.Categories.AddRange(categories);
            context.SaveChanges();
            return $"Successfully imported {categories.Count()}";
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            List<User> users = JsonConvert.DeserializeObject<List<User>>(inputJson);
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Count()}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(inputJson);
            context.Products.AddRange(products);
            context.SaveChanges();
            return $"Successfully imported {products.Count()}";
        }
    }
}