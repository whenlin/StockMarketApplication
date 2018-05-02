using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockExchangeMarket
{
    public class Company
    {

        private String _symbol;
        private String _name;
        private Double _openPrice;
        private Double _closePrice;
        private Double _currentPrice;
        private int _volume;
        private StockMarket market;
        private List<Order> BuyOrders = new List<Order>();
        private List<Order> SellOrders = new List<Order>();
        private List<Order> Transactions = new List<Order>();


        public Company(String symbol, String _name, double openPrice, StockMarket handledBy)
        {
            this._symbol = symbol;
            this._openPrice = openPrice;
            this._closePrice = 0;
            this._currentPrice = 0;
            this._volume = 0;
            this.market = handledBy;
            this._name = _name;
        }

        // Gets or sets the price
        public double lastSale
        {
            get { return _currentPrice; }
            set
            {
                if (_currentPrice != value)
                {
                    _currentPrice = value;
                    market.Notify();
                }
            }
        }

        public double OpenPrice
        {
            get { return _openPrice; }
            set { _openPrice = value; }
        }

        public double ClosePrice
        {
            get { return _closePrice; }
            set { _closePrice = value; }
        }

        public String Symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public void addBuyOrder(Double buy_price, int buy_size)
        {
            
                BuyOrder buyOrder = new BuyOrder(buy_price, buy_size);
                BuyOrders.Add(buyOrder);
                var SortedBuyOrders = BuyOrders.OrderByDescending(BuyOrder => BuyOrder.Price).ThenBy(BuyOrder => BuyOrder.TimeStamp).ToList();
                BuyOrders = SortedBuyOrders;
        }

        public void addSellOrder(Double sale_price, int sale_size)
        {

            SellOrder sellOrder = new SellOrder(sale_price, sale_size);
            SellOrders.Add(sellOrder);
            var SortedSellOrders = SellOrders.OrderBy(SellOrder => SellOrder.Price).ThenBy(SellOrder => SellOrder.TimeStamp).ToList();
            SellOrders = SortedSellOrders;
        }

        public List<Order> getBuyOrders()
        {
            return BuyOrders;
        }
        public List<Order> getSellOrders()
        {
            return SellOrders;
        }

        public void clearBuyOrders()
        {
            this.BuyOrders.Clear();
        }

        public void clearSellOrders()
        {
            this.SellOrders.Clear();
        }

        public int getVolume()
        {
            return this._volume;
        }

        public void setVolume(int volume)
        {
            this._volume = volume;
        }

        public void setLastPrice(int lastprice)
        {
            this._currentPrice = lastprice;

        }

    }
}
