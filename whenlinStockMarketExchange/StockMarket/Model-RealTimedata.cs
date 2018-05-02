using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockExchangeMarket
{
    public class RealTimedata : StockMarket
    {
        private List<Company> StockCompanies = new List<Company>();

        public void addCompany(String symbol, String _name, double price, int volume, double lastPrice)
        {
           Company _company = new Company(symbol, _name, price, this);
            _company.setVolume(volume);
            _company.lastSale = lastPrice;
           StockCompanies.Add(_company);
        }

        public List<Company> getCompanies()
        {
            return StockCompanies;
        }

        public void clearCompanies()
        {
            this.StockCompanies.Clear();
        }
    }
}
