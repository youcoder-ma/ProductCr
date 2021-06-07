using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCrud.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace ProductCrud.Services
{
    class ProductService : ContentPage
    {
        private readonly ProductContext _db;

        public ProductService()
        {
            _db = new ProductContext();
        }


        //public Task<List<Product>> GetPruducts()
        //{
        //    using (ProductContext db = new())
        //    {
        //        return Task.FromResult(db.Products
        //            .Select(p => new Product()
        //        {
        //            Id = p.Id,
        //            Name = p.Name,
        //            Description = p.Description,
        //            Price = p.Price,
        //            Quantity = p.Quantity,
        //            Photo = p.Photo
        //        }).ToList());
        //    }
        //}

        public async Task<List<Product>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }

        /*public void GetProductId(int id)
        {
            _db.Products.Find(id);
        }*/
        //public async Task<Product> AddProduct(Product product, IEnumerable<Stream> image)
        //{

        //    foreach (var file in image)
        //    {
        //        MemoryStream ms = new();
        //        await file.CopyToAsync(ms);
        //        product.Photo = ms.ToArray();
        //        await _db.Products.AddAsync(product);
        //        await DisplayAlert("Success", "Product Updated Successfully", "OK");

        //        await _db.SaveChangesAsync();

        //    }

        //    return product;
        //}

        public async Task<Product> AddProduct(Product product)
        {

            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public void DeleteProduct(Product product)
        {
            _db.Products.Remove(product);
             _db.SaveChanges();
        }
    }
}
