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
    public partial class StartInfo : Form
    {
        StockSecuritiesExchange se;
        public StartInfo()
        {
            InitializeComponent();
        }

        public void SetStockMarket(StockSecuritiesExchange se)
        {
            this.se = se;
        }

        public void GetInfo(object sender, EventArgs e)
        {
            var name = this.textBox1.Text;
            var yourIP = this.textBox2.Text;
            var yourPort = this.textBox3.Text;
            var serverIP = this.textBox4.Text;
            var serverPort = this.textBox5.Text;
            se.SetValues(name, yourIP, yourPort, serverIP, serverPort);
            this.Close();
        }
    }
}
