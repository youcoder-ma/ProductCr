using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ProductCrud.Models
{
    class ProductContext : DbContext
    {
        public ProductContext()
        {
            //this.Database.OpenConnection();
            this.Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            //if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            //{

            //    pathDB = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
            //        "Products.db");
            //}
            //else
            //{

            //    pathDB = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "..", "Library",
            //        "Products.db");
            //}
            string pathDB = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Products.db");
            optionsBuilder.UseSqlite($"filename={pathDB}");
            //string dest = ("/storage/emulated/0/Download/Products.db");
            //File.Copy(pathDB, dest);
            //base.OnConfiguring(optionsBuilder);
        }
    }
}
