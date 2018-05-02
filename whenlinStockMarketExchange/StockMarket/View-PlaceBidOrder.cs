using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockExchangeMarket
{
    public partial class PlaceBidOrder : Form
    {
        RealTimedata Subject;
        Company selectedCompany;
        StockSecuritiesExchange se;
        public PlaceBidOrder(Object _subject)
        {
            Subject = (RealTimedata)_subject;

            InitializeComponent();
            //this.comboBox1.Items.Add("");
            foreach (Company company in Subject.getCompanies())
            {
                this.comboBox1.Items.Add(company.Name);

            }
            comboBox1.SelectedIndex = 0;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void setStockMarket(StockSecuritiesExchange se)
        {
            this.se = se;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
             // Check to see if both validation checks return true
            if (ValidShareSize() && ValidSharePrice())
            {
                //selectedCompany.addBuyOrder(Convert.ToDouble(textBox2.Text), Convert.ToInt32(textBox1.Text));
                this.se.sendBidOrder(selectedCompany, Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox1.Text));
                foreach (Control control in this.Controls)
                {
                    if (control is TextBox)
                    {
                        TextBox textBox = (TextBox)control;
                        textBox.Text = null;
                    }

                    if (control is ComboBox)
                    {
                        ComboBox comboBox = (ComboBox)control;
                        if (comboBox.Items.Count > 0)
                            comboBox.SelectedIndex = 0;
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            int i = 0;
            foreach (Company company in Subject.getCompanies())
            {
                if (comboBox1.SelectedIndex == i)
                {
                    selectedCompany = company;
                    break;
                }
                else i++;
            }
        }

        // Method that validates the shares size
        // The method returns true if the textbox passes the validation checks
        private bool ValidShareSize()
        {

            int num;
            bool isNum = Int32.TryParse(textBox1.Text.Trim(), out num);

            if (!isNum)
            {
                // If not numeric, set the error
                epErrorProvider.SetError(textBox1, "The # of Shares is invalid");
                return false;
            }
            else
            {
                // If it has a value, clear the error
                epErrorProvider.SetError(textBox1, "");
                return true;
            }

        }

        // Method that validates the shares Price
        // The method returns true if the textbox passes the validation checks
        private bool ValidSharePrice()
        {

            Double Doub;
            bool isDoub = Double.TryParse(textBox2.Text.Trim(), out Doub);
            // Check the Name text
            if (!isDoub)
            {
                // If empty, set the error
                epErrorProvider.SetError(textBox2, "The Price of Shares is invalid");
                return false;
            }
            else
            {
                // If it has a value, clear the error
                epErrorProvider.SetError(textBox2, "");
                return true;
            }
        }
        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            ValidShareSize();
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            ValidSharePrice();
        }
    }
}
