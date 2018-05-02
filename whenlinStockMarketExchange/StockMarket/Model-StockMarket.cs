using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockExchangeMarket
{
    public abstract class StockMarket
    {
        //Data structure to store the observer instance (is not synchronized for this implementation)
        private List<StockMarketDisplay> StockMarketObservers = new List<StockMarketDisplay>();

        //add the observer
        public void Register(StockMarketDisplay anObserver)
        {
            StockMarketObservers.Add(anObserver);
        }//Register
        //remove the observer
        public void unRegister(StockMarketDisplay anObserver)
        {
            StockMarketObservers.Remove(anObserver);
        }//UnRegister

        //common method to notify all the observers
        public void Notify()
        {
            //enumeration the observers and invoke their notify method
            foreach (StockMarketDisplay anObserver in StockMarketObservers)
            {
                anObserver.update();
            }//foreach
        }//NotifyObservers


    }
}
