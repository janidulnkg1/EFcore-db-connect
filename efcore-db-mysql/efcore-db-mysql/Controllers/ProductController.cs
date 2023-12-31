﻿using efcore_db_mysql.Helpers;
using efcore_db_mysql.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace efcore_db_mysql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _dbContext;

        public ProductController(ProductDbContext productDbContext)
        {
            _dbContext = productDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return _dbContext.Products;
        }

        [HttpGet("{productId:int}")]
        public async Task<ActionResult<Product>> GetById(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            return product;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return Ok("Product Created Successfully!");

        }

        [HttpPut]
        public async Task<ActionResult> Update(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return Ok("Product updated successfully!");
        }

        [HttpDelete("{productId:int}")]
        public async Task<ActionResult> Delete(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return Ok("Product has been deleted successfully!");
        }


    }
}
