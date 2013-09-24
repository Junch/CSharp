using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MyFirstTest
{
    public class MyTest
    {
        [Test]
        public void Test1()
        {
            Console.WriteLine("Test1 Pass");
        }

        [Test]
        public void Test2()
        {
            Console.WriteLine("Test2 Fail");
            Assert.Fail();
        }

        [Test]
        public void Test3()
        {
            Console.WriteLine("Test3 Ignore");
            Assert.Ignore();
        }
    }
}
