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
    public partial class MarketByOrder : Form, StockMarketDisplay
    {
        RealTimedata StockMarket;
        String companyName;
        Company selectedCompany;

        public MarketByOrder(Object _subject, String Name)
        {
            Subject = _subject;
            companyName = Name;

            //Register itself as an observer
            StockMarket.Register(this);
            InitializeComponent();
            update();
        }

        public Object Subject
        {
            set
            {
                StockMarket = (RealTimedata)value;
            }
        }
        public void update()
        {
            ordersGrid.Rows.Clear();
            ordersGrid.Refresh();

            foreach (Company company in StockMarket.getCompanies())
            {
                if (company.Name == companyName)
                {
                    selectedCompany = company;
                    break;
                }
            }
            int i = 0;
            foreach (Order buyOrder in selectedCompany.getBuyOrders())
            {
                string[] row1 = { buyOrder.Size.ToString(), buyOrder.Price.ToString(),null,null };
                ordersGrid.Rows.Add(row1);
                i++;
                if (i == 10) break;
            }
            for (int j=i; j<10; j++){
                string[] row1 = { null, null, null, null };
                ordersGrid.Rows.Add(row1);
            }
            int k = 0;
            foreach (Order sellOrder in selectedCompany.getSellOrders())
            {
                ordersGrid[2, k].Value = sellOrder.Price.ToString();
                ordersGrid[3, k].Value = sellOrder.Size.ToString();
                k++;
                if (k == 10) break;
            }
        }

        private void MarketByOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            StockMarket.unRegister(this);
        }
    }
}
