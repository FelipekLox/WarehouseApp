using System;

namespace WarehouseApp
{
    public class Product
    {
        private string _sku;
        private string _name;
        private decimal _price;
        private int _quantity;
        private int _minStock;

        public Product(string sku, string name, decimal price, int quantity, int minStock = 0)
        {
            Sku = sku;
            Name = name;
            Price = price;
            Quantity = quantity;
            MinStock = minStock;
        }

        public string Sku
        {
            get { return _sku; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Артикул не может быть пустым.");
                _sku = value.Trim().ToUpper();
            }
        }

        public string Name
        {
            get { return _name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Наименование не может быть пустым.");
                _name = value.Trim();
            }
        }

        public decimal Price
        {
            get { return _price; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Цена должна быть больше нуля.");
                _price = value;
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Остаток не может быть отрицательным.");
                _quantity = value;
            }
        }

        public decimal TotalPrice
        {
            get { return _price * _quantity; }
        }

        public bool IsOutOfStock
        {
            get { return _quantity == 0; }
        }

        public int MinStock
        {
            get { return _minStock; }
            private set
            {
                if (value < 0 || value > 100000)
                    throw new ArgumentOutOfRangeException(nameof(value), "Минимальный остаток должен быть от 0 до 100 000.");
                _minStock = value;
            }
        }

        public bool IsBelowMinStock
        {
            get { return _quantity < _minStock; }
        }

        public void AddStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Количество при приёмке должно быть больше нуля.");

            Quantity = Quantity + amount;
        }

        public void RemoveStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Количество при отгрузке должно быть больше нуля.");

            if (amount > Quantity)
                throw new InvalidOperationException($"Недостаточно товара: на складе {Quantity}, запрошено {amount}.");

            Quantity = Quantity - amount;
        }

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice > _price * 2m)
                throw new InvalidOperationException("Цена не может вырасти более чем в 2 раза за одну операцию.");

            Price = newPrice;
        }

        public void SetMinStock(int value)
        {
            MinStock = value;
        }
    }
}