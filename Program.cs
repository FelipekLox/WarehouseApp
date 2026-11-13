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
            Product p = new Product("sku-001", "Ноутбук", 75000m, 10);
            Console.WriteLine($"{p.Sku}: {p.Name}, цена: {p.Price}, остаток: {p.Quantity}");
        }
    }
}