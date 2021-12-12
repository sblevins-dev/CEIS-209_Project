using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blevins_CourseProject_part2
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void hourlyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (hourlyRadioButton.Checked)
            {
                hourlyRateLabel.Visible = true;
                hourlyRateTextBox.Visible = true;
                hoursWorkedLabel.Visible = true;
                hoursWorkedTextBox.Visible = true;
                salaryLabel.Visible = false;
                salaryTextBox.Visible = false;
            }
        }

        private void salaryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (salaryRadioButton.Checked)
            {
                hourlyRateLabel.Visible = false;
                hourlyRateTextBox.Visible = false;
                hoursWorkedLabel.Visible =  false;
                hoursWorkedTextBox.Visible = false;
                salaryLabel.Visible = true;
                salaryTextBox.Visible = true;
            }
        }

    }
}
