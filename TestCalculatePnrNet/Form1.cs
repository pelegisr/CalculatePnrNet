using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Peleg.CalculatePnrNet;
using Peleg.CalculatePnrNet.Model;

namespace TestCalculatePnrNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculateGross_Click(object sender, EventArgs e)
        {
            lblResult.Text = "";
            lblMessage.Text = "";

            if (!int.TryParse(txtPnr.Text.Trim(), out int pnrId) || pnrId <= 0)
            {
                MessageBox.Show("Please enter a valid PNR ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string cs = uPanel.Connection.SqlConnectionString;
                var calculator = new CalculatePNR(ref cs);
                CalculationResult result = calculator.Calculate(pnrId);
                if (!result.Success)
                {
                    MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lblMessage.Text = $"Gross Amount: {result.Value}";
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("PNR not found. Please check the PNR ID and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid PNR ID. Please enter a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("An unexpected error occurred.", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
