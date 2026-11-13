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
    // Приёмка товара на склад
    public void AddStock(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Количество при приёмке должно быть больше нуля.");

        Quantity = Quantity + amount; // идём через свойство — проверка сработает ещё раз
    }

    // Отгрузка товара со склада
    public void RemoveStock(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Количество при отгрузке должно быть больше нуля.");

        if (amount > Quantity)
            throw new InvalidOperationException($"Недостаточно товара: на складе {Quantity}, запрошено {amount}.");

        Quantity = Quantity - amount;
    }

    // Изменение цены по правилам бизнеса, а не «просто присваивание»
    public void ChangePrice(decimal newPrice)
    {
        if (newPrice > _price * 2m)
            throw new InvalidOperationException("Цена не может вырасти более чем в 2 раза за одну операцию.");

        Price = newPrice;
    }
}