using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Blevins_CourseProject_part2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void WriteEmpsToFile()
        {
            string FILE_NAME = "Employees.csv";

            List<Employee> empList = new List<Employee>();

            foreach (Employee emp in EmployeesListBox.Items)
            {
                empList.Add(emp);
            }

            // open a pipe to the file and create a translator
            FileStream fs = new FileStream(FILE_NAME, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            // write the entire object to the file in one line
            formatter.Serialize(fs, empList);

            // close the pipe
            fs.Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            // Add item to the employee listbox
            InputForm frmInput = new InputForm();

            using (frmInput)
            {
                DialogResult result = frmInput.ShowDialog();

                // See if input form was cancelled
                if (result == DialogResult.Cancel)
                    return;  // End the method since the user cancelled the input

                // Get user's input and create an Employee object
                string fName = frmInput.FirstNameTextBox.Text;
                string lName = frmInput.LastNameTextBox.Text;
                string ssn = frmInput.SSNTextBox.Text;
                string date = frmInput.HireDateTextBox.Text;
                DateTime hireDate = DateTime.Parse(date);
                string healthIns = frmInput.healthInsuranceTextBox.Text;
                double lifeIns = Convert.ToDouble(frmInput.lifeInsuranceTextBox.Text);
                int vacation = Convert.ToInt32(frmInput.vacationTextBox.Text);

                Benefits benefits = new Benefits(healthIns, lifeIns, vacation);
                Employee emp = null;
                if (frmInput.hourlyRadioButton.Checked)
                {
                    double hourlyRate = Double.Parse(frmInput.hourlyRateTextBox.Text);
                    double hoursWorked = Double.Parse(frmInput.hoursWorkedTextBox.Text);
                    emp = new Hourly(fName, lName, ssn, hireDate, 
                        benefits, hourlyRate, hoursWorked);
                }
                else if (frmInput.salaryRadioButton.Checked)
                {
                    double annualSalary = Double.Parse(frmInput.salaryTextBox.Text);
                    emp = new Salary(fName, lName, ssn, hireDate, benefits, annualSalary);
                }
                else
                {
                    MessageBox.Show("Error, Employee type must be selected");
                    return; // end the method
                }

                // Add the Employee object to the employees listbox
                EmployeesListBox.Items.Add(emp);

                // Write employees to a file
                WriteEmpsToFile();
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            // remove the selected item from the employee listbox
            int itemNumber = EmployeesListBox.SelectedIndex;

            if (itemNumber > -1)
            {
                EmployeesListBox.Items.RemoveAt(itemNumber);

                // Write employees to a file
                WriteEmpsToFile();
            }
            else
            {
                MessageBox.Show("Please select employee to remove.");
            }
        }

        private void DisplayButton_Click(object sender, EventArgs e)
        {
            // Clear the listBox
            EmployeesListBox.Items.Clear();

            ReadEmpsFromFile();
        }

        private void ReadEmpsFromFile()
        {
            // Read all Employee objects from the file
            string FILE_NAME = "Employees.csv";

            // clear the listbox
            EmployeesListBox.Items.Clear();

            // check to see if the file exists
            if (File.Exists(FILE_NAME) && new FileInfo(FILE_NAME).Length > 0)
            {
                // create a pipe from the file and create a translator
                FileStream fs = new FileStream(FILE_NAME, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                // read the Employee list from the file
                List<Employee> list = (List<Employee>)formatter.Deserialize(fs);

                // close the pipe
                fs.Close();

                // copy each Employee object to the listbox
                foreach (Employee emp in list)
                {
                    EmployeesListBox.Items.Add(emp);
                }
            }
                
            
        }

        private void PrintPaychecksButton_Click(object sender, EventArgs e)
        {
            foreach(Employee emp in EmployeesListBox.Items)
            {
                string output = emp.FirstName + " " + emp.LastName + "\n";
                output += "SSN: " + emp.SSN + "\n";
                output += "Hire Date: " + emp.HireDate.ToShortDateString() + "\n";
                output += "Pay Ammount: " + emp.CalculatePay().ToString("C2");
                MessageBox.Show(output);
            }
        }

        private void EmployeesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Employee emp = (Employee)EmployeesListBox.SelectedItem;

            if (emp != null)
            {
                InputForm updateForm = new InputForm();

                updateForm.Text = "Update employee Information";
                updateForm.SubmitButton.Text = "Update";
                updateForm.StartPosition = FormStartPosition.CenterParent;
                updateForm.FirstNameTextBox.Text = emp.FirstName;
                updateForm.LastNameTextBox.Text = emp.LastName;
                updateForm.SSNTextBox.Text = emp.SSN;
                updateForm.HireDateTextBox.Text = emp.HireDate.ToShortDateString();
                updateForm.healthInsuranceTextBox.Text = emp.Benefits.HealthInsurance;
                updateForm.lifeInsuranceTextBox.Text = emp.Benefits.LifeInsurance.ToString("f2");
                updateForm.vacationTextBox.Text = emp.Benefits.Vacation.ToString();

                DialogResult result = updateForm.ShowDialog();

                // if cancelled, stop the method
                if (result == DialogResult.Cancel)
                    return; // ends the method

                // Delete selected object
                int position = EmployeesListBox.SelectedIndex;
                EmployeesListBox.Items.RemoveAt(position);


                // Create new employee using update information
                Employee newEmp = new Employee();
                newEmp.FirstName = updateForm.FirstNameTextBox.Text;
                newEmp.LastName = updateForm.LastNameTextBox.Text;
                newEmp.SSN = updateForm.SSNTextBox.Text;
                newEmp.HireDate = DateTime.Parse(updateForm.HireDateTextBox.Text);
                newEmp.Benefits.HealthInsurance = updateForm.healthInsuranceTextBox.Text;
                newEmp.Benefits.LifeInsurance = Double.Parse(updateForm.lifeInsuranceTextBox.Text);
                newEmp.Benefits.Vacation = int.Parse(updateForm.vacationTextBox.Text);

                // Add new employee to listBox
                EmployeesListBox.Items.Add(newEmp);
            }    
        }

        //private void MainForm_Load(object sender, EventArgs e)
        //{
        //    ReadEmpsFromFile();
        //}
    }
}
