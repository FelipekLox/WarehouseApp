using System;

namespace WarehouseApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Warehouse warehouse = new Warehouse("ЛЦ «Восточный»", 10);

            Product drill = new Product("sku-001", "Дрель аккумуляторная", 7490m, 12);
            Product paint = new Product("sku-002", "Краска фасадная, 10 л", 1890m, 40);

            warehouse.Add(drill);
            warehouse.Add(paint);

            drill.RemoveStock(5);
            paint.AddStock(10);

            Console.WriteLine($"Позиций на складе: {warehouse.Count} из {warehouse.Capacity}");
            Console.WriteLine($"Суммарная стоимость: {warehouse.GetTotalValue():N2} руб.");

            try
            {
                drill.RemoveStock(1000);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }

            foreach (Product p in warehouse.GetProducts())
            {
                Console.WriteLine($"{p.Sku} | {p.Name} | {p.Quantity} шт. | {p.TotalPrice:N2} руб.");
            }

            drill.SetMinStock(20);

            Console.WriteLine("\nПозиции ниже минимального остатка:");
            foreach (Product p in warehouse.GetProducts())
            {
                if (p.IsBelowMinStock)
                {
                    Console.WriteLine($"{p.Sku} | {p.Name} | Остаток: {p.Quantity} (мин: {p.MinStock})");
                }
            }
        }
    }
}