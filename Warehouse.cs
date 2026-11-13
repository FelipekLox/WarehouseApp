using System;

namespace WarehouseApp
{
    public class Warehouse
    {
        private readonly Product[] _products;
        private int _count;

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

        public string Name { get; }
        public int Count { get { return _count; } }
        public int Capacity { get { return _products.Length; } }
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
            if (string.IsNullOrWhiteSpace(sku))
                return null;

            string normalizedSku = sku.Trim().ToUpper();
            for (int i = 0; i < _count; i++)
            {
                if (_products[i].Sku == normalizedSku)
                    return _products[i];
            }
            return null;
        }

        public void Remove(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("Артикул для удаления не может быть пустым.", nameof(sku));

            string normalizedSku = sku.Trim().ToUpper();
            int index = -1;

            for (int i = 0; i < _count; i++)
            {
                if (_products[i].Sku == normalizedSku)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                throw new InvalidOperationException($"Товар с артикулом {normalizedSku} не найден на складе.");

            for (int i = index; i < _count - 1; i++)
            {
                _products[i] = _products[i + 1];
            }

            _count = _count - 1;
            _products[_count] = null;
        }

        public decimal GetTotalValue()
        {
            decimal total = 0;
            for (int i = 0; i < _count; i++)
                total = total + _products[i].TotalPrice;
            return total;
        }

        public int GetTotalQuantity()
        {
            int totalQuantity = 0;
            for (int i = 0; i < _count; i++)
                totalQuantity = totalQuantity + _products[i].Quantity;
            return totalQuantity;
        }

        public Product[] GetProducts()
        {
            Product[] copy = new Product[_count];
            for (int i = 0; i < _count; i++)
                copy[i] = _products[i];
            return copy;
        }

        public Product[] GetLowStockProducts()
        {
            int lowCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_products[i].IsBelowMinStock)
                    lowCount++;
            }

            Product[] lowStockCopy = new Product[lowCount];
            int currentIndex = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_products[i].IsBelowMinStock)
                {
                    lowStockCopy[currentIndex] = _products[i];
                    currentIndex++;
                }
            }
            return lowStockCopy;
        }
    }
}