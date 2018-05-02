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
    public partial class MarketByPrice : Form, StockMarketDisplay
    {

        RealTimedata StockMarket;
        String companyName;
        Company selectedCompany;

        public MarketByPrice(Object _subject, String Name)
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


            int[] buy_count = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Double[] buy_price = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] buy_volume = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int bi = 0;

            var firstBuyPrice = selectedCompany.getBuyOrders().FirstOrDefault();
            if (firstBuyPrice != null)
            {
                buy_price[bi] = firstBuyPrice.Price;
            }

            foreach (Order B in selectedCompany.getBuyOrders())
            {
                if (buy_price[bi] == B.Price)
                {
                    buy_count[bi]++;
                    buy_volume[bi] += B.Size;
                }
                else
                {
                    bi++;
                    buy_count[bi] = 1;
                    buy_price[bi] = B.Price;
                    buy_volume[bi] = B.Size;
                }
            }

            for (int j = 0; j < 10; j++)
            {
                string[] row1 = { (buy_price[j] == 0 ? "" : buy_count[j].ToString()), 
                                        (buy_volume[j] == 0 ? "" : buy_volume[j].ToString()), 
                                        (buy_price[j] == 0 ? "" : buy_price[j].ToString())};
                ordersGrid.Rows.Add(row1);

            }


            int[] sell_count = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Double[] sell_price = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] sell_volume = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int si = 0;

            var firstSellPrice = selectedCompany.getSellOrders().FirstOrDefault();
            if (firstSellPrice != null)
            {
                sell_price[si] = firstSellPrice.Price;
            }

            foreach (Order S in selectedCompany.getSellOrders())
            {
                if (sell_price[si] == S.Price)
                {
                    sell_count[si]++;
                    sell_volume[si] += S.Size;
                }
                else
                {
                    si++;
                    sell_count[si] = 1;
                    sell_price[si] = S.Price;
                    sell_volume[si] = S.Size;
                }
            }

            for (int k = 0; k < 10; k++)
            {
                ordersGrid[3, k].Value = (sell_price[k] == 0 ? "" : sell_price[k].ToString());
                ordersGrid[4, k].Value = (sell_volume[k] == 0 ? "" : sell_volume[k].ToString());
                ordersGrid[5, k].Value = (sell_price[k] == 0 ? "" : sell_count[k].ToString());
            }

        }

        private void MarketByPrice_FormClosed(object sender, FormClosedEventArgs e)
        {
            StockMarket.unRegister(this);
        }

        private void MarketByPrice_Load(object sender, EventArgs e)
        {

        }
    }
}
