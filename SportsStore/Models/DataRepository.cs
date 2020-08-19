using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class DataRepository : IRepository
    {
        private DataContext _context;

        public DataRepository(DataContext ctx)
            => _context = ctx;




        public IEnumerable<Product> Products => _context.Products
            .Include(p => p.Category).ToArray();




        public Product GetProduct(long key)
            => _context.Products
            .Include(p => p.Category).First(p => p.Id == key);



        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }



        public void UpdateProduct(Product product)
        {
            Product p = _context.Products.Find(product.Id);
            p.Name = product.Name;
            p.PurchasePrice = product.PurchasePrice;
            p.RetailPrice = product.RetailPrice;
            p.CategoryId = product.CategoryId;
            //_context.Products.Update(product); // Помоему это круче, подумать.
            _context.SaveChanges();
        }




        public void UpdateAll(Product[] SendedProducts)
        {
            Dictionary<long, Product> data = SendedProducts.ToDictionary(p => p.Id);
            IEnumerable<Product> baseline =
                _context.Products.Where(p => data.Keys.Contains(p.Id));

            foreach(Product databaseProduct in baseline)
            {
                Product requestProduct = data[databaseProduct.Id];
                databaseProduct.Name = requestProduct.Name;
                databaseProduct.Category = requestProduct.Category;
                databaseProduct.PurchasePrice = requestProduct.PurchasePrice;
                databaseProduct.RetailPrice = requestProduct.RetailPrice;
            }
            //_context.Products.UpdateRange(SendedProducts);
            _context.SaveChanges();
        }



        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
