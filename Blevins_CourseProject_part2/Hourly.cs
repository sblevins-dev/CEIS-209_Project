using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blevins_CourseProject_part2
{
    [Serializable]
    class Hourly : Employee
    {
        // Attributes
        private double hourlyRate;
        private double hoursWorked;

        // Constructors
        public Hourly() : base()
        {
            HourlyRate = 0.0;
            HoursWorked = 0.0;
        }

        public Hourly(string firstName, string lastName, string ssn, 
            DateTime hireDate, Benefits benefits, double hourlyRate, double hoursWorked) 
            : base(firstName, lastName, ssn, hireDate, benefits)
        {
            this.HourlyRate = hourlyRate;
            this.HoursWorked = hoursWorked;
        }

        // Behaviors
        public override double CalculatePay()
        {
            return HourlyRate * HoursWorked;
        }

        public override string ToString()
        {
            return base.ToString() 
                + ", hourlyRate= " + HourlyRate.ToString("f2")
                + ", hoursWorked= " + HoursWorked.ToString("f1");
        }

        // Properties
        public double HourlyRate { get => hourlyRate; set => hourlyRate = value; }
        public double HoursWorked { get => hoursWorked; set => hoursWorked = value; }
    }
}
