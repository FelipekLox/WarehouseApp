using System;

namespace WarehouseApp
{
    public class Supplier
    {
        private string _name;
        private string _phone;
        private string _inn;

        public Supplier(string name, string phone, string inn)
        {
            Name = name;
            Phone = phone;
            Inn = inn;
        }

        public string Name
        {
            get { return _name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Название поставщика не может быть пустым.");
                _name = value.Trim();
            }
        }

        public string Phone
        {
            get { return _phone; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Телефон не может быть пустым.");

                string trimmed = value.Trim();
                foreach (char c in trimmed)
                {
                    if (!char.IsDigit(c) && c != '+' && c != '-' && c != ' ')
                        throw new ArgumentException("Телефон может содержать только цифры, '+', '-' и пробелы.");
                }
                _phone = trimmed;
            }
        }

        public string Inn
        {
            get { return _inn; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ИНН не может быть пустым.");

                string trimmed = value.Trim();
                if (trimmed.Length != 10 && trimmed.Length != 12)
                    throw new ArgumentException("ИНН должен содержать ровно 10 или 12 цифр.");

                foreach (char c in trimmed)
                {
                    if (!char.IsDigit(c))
                        throw new ArgumentException("ИНН должен состоять только из цифр.");
                }
                _inn = trimmed;
            }
        }
    }
}