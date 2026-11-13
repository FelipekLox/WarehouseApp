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

            // Задача 1: Минимальный остаток
            drill.SetMinStock(20);

            Console.WriteLine("\nПозиции ниже минимального остатка:");
            foreach (Product p in warehouse.GetProducts())
            {
                if (p.IsBelowMinStock)
                {
                    Console.WriteLine($"{p.Sku} | {p.Name} | Остаток: {p.Quantity} (мин: {p.MinStock})");
                }
            }

            // Задача 2: Поставщик
            Supplier bosch = new Supplier("ООО «Роберт Бош»", "+7 495 560-95-60", "7712013895");
            Supplier tikkurila = new Supplier("ООО «Тиккурила»", "+7 812 380-33-99", "7816056898");

            drill.AssignSupplier(bosch);
            paint.AssignSupplier(tikkurila);

            Console.WriteLine("\nТовары и их поставщики:");
            foreach (Product p in warehouse.GetProducts())
            {
                string supplierInfo = p.Supplier != null
                    ? $"{p.Supplier.Name} (ИНН: {p.Supplier.Inn}, тел: {p.Supplier.Phone})"
                    : "не назначен";
                Console.WriteLine($"{p.Sku} | {p.Name} | Поставщик: {supplierInfo}");
            }

            try
            {
                drill.AssignSupplier(null);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Ошибка назначения поставщика: " + ex.ParamName);
            }

            // Задача 3: Списание с указанием причины
            drill.WriteOff(2, "повреждение при транспортировке");
            Console.WriteLine($"\nСписание выполнено: {drill.Name} | Остаток: {drill.Quantity} шт. | Всего списано: {drill.WrittenOffTotal} шт.");

            try
            {
                drill.WriteOff(1, " ");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Ошибка списания: " + ex.Message);
            }

            // Задача 4: Удаление позиции и отчёт по «дефициту»
            Console.WriteLine($"\nВсего единиц товара на складе: {warehouse.GetTotalQuantity()} шт.");

            Console.WriteLine("Отчёт по дефициту (GetLowStockProducts):");
            foreach (Product p in warehouse.GetLowStockProducts())
            {
                Console.WriteLine($"[ДЕФИЦИТ] {p.Sku} | {p.Name} | Остаток: {p.Quantity} (мин: {p.MinStock})");
            }

            Product[] snapshotBeforeRemove = warehouse.GetProducts();
            warehouse.Remove("SKU-002");

            Console.WriteLine($"\nПосле удаления SKU-002:");
            Console.WriteLine($"Позиций на складе (warehouse.Count): {warehouse.Count}");
            Console.WriteLine($"Длина сохранённой ранее копии массива: {snapshotBeforeRemove.Length}");
        }
    }
}