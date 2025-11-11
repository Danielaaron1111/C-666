using CoffeShop.Components;

namespace CoffeeShopSystem
{
    public class CoffeeOrder
    {
        // Private fields
        private string _OrderId;
        private string _CustomerName;
        private int _Quantity;
        private double _Price;

        // Constants
        private const int MINIMUM_QUANTITY = 1;
        private const int MAXIMUM_QUANTITY = 20;
        private const double MINIMUM_PRICE = 0.01;
        private const double MAXIMUM_PRICE = 100.00;

        // Properties
        public string OrderId
        {
            get { return _OrderId; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Order ID is required");
                _OrderId = value.Trim();
            }
        }

        public string CustomerName
        {
            get { return _CustomerName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Customer name is required");
                _CustomerName = value.Trim();
            }
        }

        public CoffeeType Type { get; private set; }

        public CoffeeSize Size { get; private set; }

        public int Quantity
        {
            get { return _Quantity; }
            private set
            {
                if (!Utilities.IsNonZeroPositive(value))
                    throw new ArgumentException($"Quantity {value} must be positive");

                if (value < MINIMUM_QUANTITY)
                    throw new ArgumentException($"Quantity must be at least {MINIMUM_QUANTITY}");

                if (value > MAXIMUM_QUANTITY)
                    throw new ArgumentException($"Quantity cannot exceed {MAXIMUM_QUANTITY}");

                _Quantity = value;
            }
        }

        public double Price
        {
            get { return _Price; }
            private set
            {
                if (!Utilities.IsValidPrice(value))
                    throw new ArgumentException($"Price {value:C} must be between ${MINIMUM_PRICE:F2} and ${MAXIMUM_PRICE:F2}");

                _Price = value;
            }
        }

        // Constructor
        public CoffeeOrder(string orderId, string customerName,
                          CoffeeType type, CoffeeSize size,
                          int quantity, double price)
        {
            OrderId = orderId;
            CustomerName = customerName;
            Type = type;
            Size = size;
            Quantity = quantity;
            Price = price;
        }

        // ToString - converts to CSV format
        public override string ToString()
        {
            return $"{OrderId},{CustomerName},{Type},{Size},{Quantity},{Price}";
        }

        // Parse - converts CSV string to object
        public static CoffeeOrder Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("No data supplied");

            string[] data = value.Split(',');

            if (data.Length != 6)
                throw new FormatException($"Incorrect format - expected 6 fields, got {data.Length}");

            return new CoffeeOrder(
                data[0].Trim(),
                data[1].Trim(),
                (CoffeeType)Enum.Parse(typeof(CoffeeType), data[2]),
                (CoffeeSize)Enum.Parse(typeof(CoffeeSize), data[3]),
                int.Parse(data[4]),
                double.Parse(data[5])
            );
        }
    }
}