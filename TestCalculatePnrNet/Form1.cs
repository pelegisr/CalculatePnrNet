using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Peleg.CalculatePnrNet;
using Peleg.CalculatePnrNet.Services;

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
                using (CalculateService calculateService = new CalculateService(uPanel.Connection.SqlConnectionString, pnrId))
                {
                    decimal grossAmount = calculateService.CalculateGross();
                    lblResult.Text = grossAmount.ToString(CultureInfo.InvariantCulture);
                    lblMessage.Text = $"Gross Amount: {grossAmount}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Calculation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }

    }
}
