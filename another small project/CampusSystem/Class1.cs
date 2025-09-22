using System.Reflection.Metadata.Ecma335;

namespace CampusSystem
{
    public class Student
    {
        //constants for validation
        private const int MIN_YEAR = 2000;
        //Private backing fields
        private int _studentNumber;
        private string _fullName = string.Empty; // Initialize to avoid CS8618
        private int _enrollmentYear;

        //Next: properties, constructor, and isActive logic!

        public int StudentNumber
        {
            get { return _studentNumber; }
            set
            {
                if (value <= 0) throw new ArgumentException("Student number must be positive and non-zero.");
                _studentNumber = value;
            }
        }

        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Full name must not be empty or whitespace.");
                _fullName = value.Trim();
            }
        }


        public int EnrollmentYear
        {
            get { return _enrollmentYear; }
            set
            {
                if (value < MIN_YEAR)
                {
                    throw new ArgumentException("Students can only be enrollment from year 2000 and over");
                }
                _enrollmentYear = value;
            }
        }

        public bool IsActive
        {
            get
            {
                int currentYear = System.DateTime.Now.Year;
                // Student is active if enrolled in current year or within the past 3 years
                return EnrollmentYear >= (currentYear - 3);
            }
        }




        public Student(int studentNumber, string fullName, int enrollmentYear)
        {
            StudentNumber = studentNumber;
            FullName = fullName;
            EnrollmentYear = enrollmentYear;
        }

        public override string ToString()
        {
            return $"{StudentNumber},{FullName},{EnrollmentYear}";
        }





    }

}
