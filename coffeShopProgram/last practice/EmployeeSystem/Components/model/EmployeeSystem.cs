namespace EmployeeSystem
{
    public class EmployeeShift
    {
        // Private fields
        private string _EmployeeId;
        private string _EmployeeName;
        private DateTime _ShiftDate;
        private double _HoursWorked;
        private double _HourlyRate;

        // Constants
        private const double MINIMUM_HOURLY_RATE = 15.00;
        private const double MAXIMUM_HOURLY_RATE = 100.00;

        // Properties
        public string EmployeeId
        {
            get { return _EmployeeId; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Employee ID is required");
                _EmployeeId = value.Trim();
            }
        }

        public string EmployeeName
        {
            get { return _EmployeeName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Employee name is required");
                _EmployeeName = value.Trim();
            }
        }

        public DateTime ShiftDate
        {
            get { return _ShiftDate; }
            private set
            {
                if (!Utilities.IsValidPastDate(value))
                    throw new ArgumentException("Shift date cannot be in the future");

                if (!Utilities.IsWithinLast90Days(value))
                    throw new ArgumentException("Shift date must be within last 90 days");

                _ShiftDate = value.Date; // Store only date part (no time)
            }
        }

        public ShiftType Shift { get; private set; }

        public double HoursWorked
        {
            get { return _HoursWorked; }
            private set
            {
                if (!Utilities.IsNonZeroPositive(value))
                    throw new ArgumentException("Hours worked must be positive");

                if (!Utilities.IsValidShiftHours(value))
                    throw new ArgumentException("Hours worked must be between 1 and 16 hours");

                _HoursWorked = value;
            }
        }

        public double HourlyRate
        {
            get { return _HourlyRate; }
            private set
            {
                if (!Utilities.IsNonZeroPositive(value))
                    throw new ArgumentException("Hourly rate must be positive");

                if (value < MINIMUM_HOURLY_RATE || value > MAXIMUM_HOURLY_RATE)
                    throw new ArgumentException($"Hourly rate must be between ${MINIMUM_HOURLY_RATE} and ${MAXIMUM_HOURLY_RATE}");

                _HourlyRate = value;
            }
        }

        // Calculated property
        public double TotalPay
        {
            get { return _HoursWorked * _HourlyRate; }
        }

        // Constructor
        public EmployeeShift(string employeeId, string employeeName,
                            DateTime shiftDate, ShiftType shift,
                            double hoursWorked, double hourlyRate)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            ShiftDate = shiftDate;
            Shift = shift;
            HoursWorked = hoursWorked;
            HourlyRate = hourlyRate;
        }

        // ToString - converts to CSV format
        public override string ToString()
        {
            return $"{EmployeeId},{EmployeeName},{ShiftDate:yyyy-MM-dd},{Shift},{HoursWorked},{HourlyRate}";
        }

        // Parse - converts CSV string to object
        public static EmployeeShift Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("No data supplied");

            string[] data = value.Split(',');

            if (data.Length != 6)
                throw new FormatException($"Incorrect format - expected 6 fields, got {data.Length}");

            return new EmployeeShift(
                data[0].Trim(),
                data[1].Trim(),
                DateTime.Parse(data[2]),
                (ShiftType)Enum.Parse(typeof(ShiftType), data[3]),
                double.Parse(data[4]),
                double.Parse(data[5])
            );
        }
    }
}