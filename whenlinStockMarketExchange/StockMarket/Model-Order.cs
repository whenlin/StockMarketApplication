using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockExchangeMarket
{
    public interface Order
    {
        DateTime TimeStamp
        {
            get;
            set;
        }

        int Size
        {
            get;
            set;
        }

        Double Price
        {
            get;
            set;
        }

    }
}
