// Example is from the book - Pro ASP.NET MVC 5.5Ed
// The example is from the Chapter 6.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace MoqSample
{
    public class Product{
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { set; get; }
    }

    public interface IValueCalculator {
        decimal ValueProducts(IEnumerable<Product> products);
    }

    public interface IDiscountHelper {
        decimal ApplyDiscount(decimal totalParam);
    }

    public class DefaultDiscountHelper : IDiscountHelper {
        private decimal DiscountSize;

        public DefaultDiscountHelper(decimal discountParam) {
            DiscountSize = discountParam;
        }

        public decimal ApplyDiscount(decimal totalParam) {
            return (totalParam - (DiscountSize / 100m * totalParam));
        }
    }

    public class MinimumDiscountHelper : IDiscountHelper {
        public decimal ApplyDiscount(decimal totalParam) {
            if (totalParam < 0) {
                throw new ArgumentOutOfRangeException();
            } else if (totalParam > 100) {
                return totalParam * 0.9M;
            } else if (totalParam >= 10 && totalParam <= 100) {
                return totalParam - 5;
            } else {
                return totalParam;
            }
        }
    }

    public class LinqValueCalculator : IValueCalculator {
        private IDiscountHelper discounter;

        public LinqValueCalculator(IDiscountHelper discountParam){
            discounter = discountParam;
        }

        public decimal ValueProducts(IEnumerable<Product> products) {
            return discounter.ApplyDiscount(products.Sum(p => p.Price));
        }
    }

    public class ShoppingCart {
        private IValueCalculator calc;
        public ShoppingCart(IValueCalculator calcParam) {
            calc = calcParam;
        }

        public IEnumerable<Product> Products { get; set; }

        public decimal CalculateProductTotal() {
            return calc.ValueProducts(Products);
        }
    }

    class FirstModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IValueCalculator>().To<LinqValueCalculator>();
            Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithConstructorArgument("discountParam", 50M);
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

        private static IKernel kernel = new StandardKernel(new FirstModule());

        static void Main(string[] args)
        {
            IValueCalculator calc = kernel.Get<IValueCalculator>();
            ShoppingCart cart = new ShoppingCart(calc) { Products = products };
            decimal totalValue = cart.CalculateProductTotal();
            System.Console.WriteLine("Total valule is {0}", totalValue);
        }
    }
}
