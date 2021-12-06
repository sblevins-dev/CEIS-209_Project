using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string fileName = "Employees.csv";
            StreamWriter sw = new StreamWriter(fileName);
            for (int i = 0; i < EmployeesListBox.Items.Count; i++)
            {
                Employee temp = (Employee)EmployeesListBox.Items[i];
                sw.WriteLine(temp.FirstName + ',' + temp.LastName + ',' + temp.SSN + ',' + temp.HireDate.ToShortDateString());
            }

            sw.Close();

            // Tell user records added to file
            MessageBox.Show("Employees have been saved to a file.");
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

                Employee emp = new Employee(fName, lName, ssn, hireDate);

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

            StreamReader sr = new StreamReader(FILE_NAME);

            using (sr)
            {
                while (sr.Peek() > -1)
                {
                    // Read the line and break it into parts based on the commas
                    string line = sr.ReadLine();
                    string[] parts = line.Split(',');

                    string fName = parts[0];
                    string lName = parts[1];
                    string ssn = parts[2];
                    DateTime hireDate = DateTime.Parse(parts[3]);

                    // Create the Employee object and add it to the listBox
                    Employee emp = new Employee(fName, lName, ssn, hireDate);
                    EmployeesListBox.Items.Add(emp);
                }
            }
        }

        private void PrintPaychecksButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Printing paychecks for all employees...");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ReadEmpsFromFile();
        }
    }
}
