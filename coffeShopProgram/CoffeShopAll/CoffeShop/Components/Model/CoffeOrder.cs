using CoffeShop.Components;

namespace CoffeeShopSystem
{
    /// <summary>
    /// BUSINESS CLASS for Coffee Orders
    /// Contains: validation, properties, constructor, ToString (for CSV write), Parse (for CSV read)
    /// EXAM TIP: This is your standard business class pattern - memorize this structure!
    /// </summary>
    public class CoffeeOrder
    {
        // ============================================================
        // PRIVATE FIELDS - backing fields for properties with validation
        // ============================================================
        private string _OrderId;
        private string _CustomerName;
        private int _Quantity;
        private double _Price;

        // ============================================================
        // CONSTANTS - define business rules (min/max values)
        // EXAM TIP: Always use constants for validation limits!
        // ============================================================
        private const int MINIMUM_QUANTITY = 1;
        private const int MAXIMUM_QUANTITY = 20;
        private const double MINIMUM_PRICE = 0.01;
        private const double MAXIMUM_PRICE = 100.00;

        // ============================================================
        // PROPERTIES with VALIDATION
        // EXAM TIP: Property pattern = get returns field, set validates then assigns
        // ============================================================
        
        /// <summary>
        /// Order ID property - CANNOT be null/empty/whitespace
        /// Throws ArgumentNullException if invalid
        /// </summary>
        public string OrderId
        {
            get { return _OrderId; }
            private set  // private set = can only be set inside this class (via constructor)
            {
                // VALIDATION: Check if string is null, empty, or just spaces
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Order ID is required");
                
                // .Trim() removes leading/trailing spaces
                _OrderId = value.Trim();
            }
        }

        /// <summary>
        /// Customer Name property - CANNOT be null/empty/whitespace
        /// Throws ArgumentNullException if invalid
        /// </summary>
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

        /// <summary>
        /// Coffee Type - uses enum (no validation needed, enum guarantees valid values)
        /// EXAM TIP: Enums are self-validating!
        /// </summary>
        public CoffeeType Type { get; private set; }

        /// <summary>
        /// Coffee Size - uses enum (no validation needed)
        /// </summary>
        public CoffeeSize Size { get; private set; }

        /// <summary>
        /// Quantity property - must be positive, between 1-20
        /// Uses Utilities class for validation
        /// Throws ArgumentException if invalid
        /// </summary>
        public int Quantity
        {
            get { return _Quantity; }
            private set
            {
                // VALIDATION 1: Check if positive (> 0) using utility method
                if (!Utilities.IsNonZeroPositive(value))
                    throw new ArgumentException($"Quantity {value} must be positive");

                // VALIDATION 2: Check minimum value
                if (value < MINIMUM_QUANTITY)
                    throw new ArgumentException($"Quantity must be at least {MINIMUM_QUANTITY}");

                // VALIDATION 3: Check maximum value
                if (value > MAXIMUM_QUANTITY)
                    throw new ArgumentException($"Quantity cannot exceed {MAXIMUM_QUANTITY}");

                _Quantity = value;
            }
        }

        /// <summary>
        /// Price property - must be between $0.01 and $100.00
        /// Uses Utilities class for validation
        /// Throws ArgumentException if invalid
        /// </summary>
        public double Price
        {
            get { return _Price; }
            private set
            {
                // VALIDATION: Uses utility method to check price range
                if (!Utilities.IsValidPrice(value))
                    throw new ArgumentException($"Price {value:C} must be between ${MINIMUM_PRICE:F2} and ${MAXIMUM_PRICE:F2}");

                _Price = value;
            }
        }

        // ============================================================
        // CONSTRUCTOR - initializes all properties
        // EXAM TIP: Constructor parameters match properties, validation happens in property setters
        // ============================================================
        
        /// <summary>
        /// GREEDY CONSTRUCTOR - takes all required data
        /// Validation happens automatically through property setters
        /// If any validation fails, exception is thrown and object is NOT created
        /// </summary>
        public CoffeeOrder(string orderId, string customerName,
                          CoffeeType type, CoffeeSize size,
                          int quantity, double price)
        {
            // Setting properties (not fields) triggers validation
            OrderId = orderId;        // validates in OrderId setter
            CustomerName = customerName;  // validates in CustomerName setter
            Type = type;              // enum, no validation needed
            Size = size;              // enum, no validation needed
            Quantity = quantity;      // validates in Quantity setter
            Price = price;            // validates in Price setter
        }

        // ============================================================
        // ToString() - CONVERTS OBJECT TO CSV STRING
        // EXAM TIP: Used for WRITING to CSV file
        // ============================================================
        
        /// <summary>
        /// Converts this object to CSV format string
        /// Format: OrderId,CustomerName,Type,Size,Quantity,Price
        /// Example: "ORD001,John Smith,Espresso,Small,1,3.50"
        /// EXAM TIP: This is used when SAVING data to CSV file
        /// </summary>
        public override string ToString()
        {
            // $"..." is string interpolation - embeds variables in string
            // Fields are separated by commas (CSV = Comma Separated Values)
            return $"{OrderId},{CustomerName},{Type},{Size},{Quantity},{Price}";
        }

        // ============================================================
        // Parse() - CONVERTS CSV STRING TO OBJECT
        // EXAM TIP: Used for READING from CSV file - STATIC method!
        // ============================================================
        
        /// <summary>
        /// STATIC method that converts a CSV string into a CoffeeOrder object
        /// Format expected: "OrderId,CustomerName,Type,Size,Quantity,Price"
        /// EXAM TIP: This is used when READING data from CSV file
        /// Throws: FormatException if wrong number of fields
        ///         ArgumentNullException if data is null/empty
        ///         Other exceptions from property validation
        /// </summary>
        public static CoffeeOrder Parse(string value)
        {
            // STEP 1: Check if input string is null or empty
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("No data supplied");

            // STEP 2: Split CSV line by commas into array
            // Example: "ORD001,John,Espresso,Small,1,3.50" 
            //       -> ["ORD001", "John", "Espresso", "Small", "1", "3.50"]
            string[] data = value.Split(',');

            // STEP 3: Validate we have exactly 6 fields (OrderId, Name, Type, Size, Qty, Price)
            if (data.Length != 6)
                throw new FormatException($"Incorrect format - expected 6 fields, got {data.Length}");

            // STEP 4: Create and return new object using constructor
            // Constructor will validate all data through property setters
            return new CoffeeOrder(
                data[0].Trim(),                                      // OrderId (string)
                data[1].Trim(),                                      // CustomerName (string)
                (CoffeeType)Enum.Parse(typeof(CoffeeType), data[2]),  // Type (enum from string)
                (CoffeeSize)Enum.Parse(typeof(CoffeeSize), data[3]),  // Size (enum from string)
                int.Parse(data[4]),                                  // Quantity (string to int)
                double.Parse(data[5])                                // Price (string to double)
            );
            
            // EXAM TIP: Enum.Parse converts string "Espresso" to CoffeeType.Espresso
            // EXAM TIP: int.Parse and double.Parse convert strings to numbers
        }
    }
}