using System;

public class Product
{
    private string _sku;
    private string _name;
    private decimal _price;
    private int _quantity;

    public Product(string sku, string name, decimal price, int quantity)
    {
        Sku = sku; // присваиваем через свойства — проверки не дублируются
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    // Артикул задаётся один раз: снаружи изменить нельзя
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

    // Наименование — аналогично
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
    // Вычисляемое свойство: своего поля нет, значение выводится из других полей
    public decimal TotalPrice
    {
        get { return _price * _quantity; }
    }

    // Ещё одно «производное» свойство — просто для удобства чтения кода
    public bool IsOutOfStock
    {
        get { return _quantity == 0; }
    }
}