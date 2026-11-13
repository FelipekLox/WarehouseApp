using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Product p = new Product();
            p.Sku = "SKU-001";
            p.Name = "";
            p.Price = -1500m;
            p.Quantity = -50;

            Console.WriteLine(p.Price * p.Quantity);
        }
    }
}