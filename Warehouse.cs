using System;

namespace WarehouseApp
{
    public class Warehouse
    {
        private readonly Product[] _products; // readonly — саму ссылку заменить нельзя
        private int _count;                   // сколько ячеек массива реально занято

        public Warehouse(string name, int capacity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название склада не может быть пустым.");
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Ёмкость склада должна быть больше нуля.");

            Name = name.Trim();
            _products = new Product[capacity];
            _count = 0;
        }

        public string Name { get; }                         // только чтение
        public int Count { get { return _count; } }         // занято позиций
        public int Capacity { get { return _products.Length; } } // всего ячеек
        public bool IsFull { get { return _count == _products.Length; } }

        public void Add(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (IsFull)
                throw new InvalidOperationException("Склад заполнен.");
            if (Find(product.Sku) != null)
                throw new InvalidOperationException($"Товар с артикулом {product.Sku} уже есть на складе.");

            _products[_count] = product;
            _count = _count + 1;
        }

        public Product Find(string sku)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_products[i].Sku == sku)
                    return _products[i];
            }
            return null;
        }

        public decimal GetTotalValue()
        {
            decimal total = 0;
            for (int i = 0; i < _count; i++)
                total = total + _products[i].TotalPrice;
            return total;
        }

        // ВАЖНО: наружу уходит КОПИЯ, а не внутренний массив
        public Product[] GetProducts()
        {
            Product[] copy = new Product[_count];
            for (int i = 0; i < _count; i++)
                copy[i] = _products[i];
            return copy;
        }
    }
}