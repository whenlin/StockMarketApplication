using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockExchangeMarket
{
    public interface StockMarketDisplay
    {
        Object Subject
        {
            set;
        }

        void update();
    }
}
