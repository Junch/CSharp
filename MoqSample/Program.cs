// Example is from the book - Pro ASP.NET MVC 5.5Ed
// The example is from the Chapter 6.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample
{
    public class Product{
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { set; get; }
    }

    public interface IValueCalculator
    {
        decimal ValueProducts(IEnumerable<Product> products);
    }

    public class LinqValueCalculator : IValueCalculator
    {
        public decimal ValueProducts(IEnumerable<Product> products)
        {
            return products.Sum(p => p.Price);
        }
    }

    public class ShoppingCart
    {
        private IValueCalculator calc;
        public ShoppingCart(IValueCalculator calcParam)
        {
            calc = calcParam;
        }

        public IEnumerable<Product> Products { get; set; }

        public decimal CalculateProductTotal()
        {
            return calc.ValueProducts(Products);
        }
    }

    class Program
    {
        private static Product[] products = {
            new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
            new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
            new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
            new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
        };

        static void Main(string[] args)
        {
            IValueCalculator calc = new LinqValueCalculator();
            ShoppingCart cart = new ShoppingCart(calc) { Products = products };
            decimal totalValue = cart.CalculateProductTotal();
            System.Console.WriteLine("Total valule is {0}", totalValue);
        }
    }
}
