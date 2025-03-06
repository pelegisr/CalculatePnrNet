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
using Peleg.CalculatePnrNet.Data;
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
                //TODO: Specify the connection string here and make PnrDbContext private in the dll
                using (var context = new PnrDbContext())
                {
                    var calculator = new CalculatePNR(context);
                    decimal grossAmount = calculator.CalcGross(pnrId);
                    decimal net = calculator.CalcNet(pnrId);

                    lblMessage.Text = $"Gross Amount: {grossAmount}";
                }

                //using (CalculateService calculateService = new CalculateService(uPanel.Connection.SqlConnectionString, pnrId))
                //{
                //    decimal grossAmount = calculateService.CalculateGross();
                //    lblResult.Text = grossAmount.ToString(CultureInfo.InvariantCulture);
                //    lblMessage.Text = $"Gross Amount: {grossAmount}";
                //}
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
