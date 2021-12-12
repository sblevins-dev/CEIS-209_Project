using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blevins_CourseProject_part2
{
    [Serializable]
    class Salary : Employee
    {
        // Attributes
        private double annualSalary;

        // Constructors
        public Salary() : base()
        {
            AnnualSalary = 0.0;
        }

        public Salary(string firstName, string lastName, string ssn, 
            DateTime hireDate, Benefits benefits, double annualSalary) 
            : base(firstName, lastName, ssn, hireDate, benefits)
        {
            this.AnnualSalary = annualSalary;
        }

        // Behaviors
        public override double CalculatePay()
        {
            return AnnualSalary / 26.0;
        }

        public override string ToString()
        {
            return base.ToString() + ", annualSalary= " 
                + AnnualSalary.ToString("f2");
        }

        // Properties
        public double AnnualSalary { get => annualSalary; set => annualSalary = value; }
    }
}
