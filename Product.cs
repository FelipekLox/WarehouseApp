using System;

public class Product
{
    private string _sku;
    private string _name;
    private decimal _price;
    private int _quantity;

    public string Sku
    {
        get { return _sku; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Артикул не может быть пустым.");
            _sku = value.Trim().ToUpper();
        }
    }

    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Наименование не может быть пустым.");
            _name = value.Trim();
        }
    }

    public decimal Price
    {
        get { return _price; }
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Цена должна быть больше нуля.");
            _price = value;
        }
    }

    public int Quantity
    {
        get { return _quantity; }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Остаток не может быть отрицательным.");
            _quantity = value;
        }
    }
}