using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blevins_CourseProject_part2
{
    [Serializable]
    class Benefits
    {
        // Attributes
        private string healthInsurance;
        private double lifeInsurance;
        private int vacation;

        // Constructors
        public Benefits()
        {
            HealthInsurance = "unknown";
            LifeInsurance = 0.0;    // double result = 9 / 10; // 0 results in whole number
            Vacation = 0;
        }

        public Benefits(string healthInsurance, double lifeInsurance, int vacation)
        {
            this.HealthInsurance = healthInsurance;
            this.LifeInsurance = lifeInsurance;
            this.Vacation = vacation;
        }

        // Behaviors
        public override string ToString()
        {
            return "healthInsurance= " + HealthInsurance 
                + ", lifeInsurance= " + LifeInsurance.ToString("f0")
                + ", vacation= " + Vacation; 
        }

        // Properties
        public string HealthInsurance { get => healthInsurance; set => healthInsurance = value; }

        public double LifeInsurance { get => lifeInsurance; set => lifeInsurance = value; }

        public int Vacation { get => vacation; set => vacation = value; }
    }
}
