using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StockExchangeMarket
{

    public partial class StockSecuritiesExchange : Form
    {
        string clientName, clientIP, serverIP;
        int clientPort = 6000, serverPort;
        RealTimedata Subject;
        Socket mySocket = null;
        Socket receiverSocket = null;
        string session = "0";
        int CSeq = 0;
        string companyList;
        public StockSecuritiesExchange()
        {
            InitializeComponent();

            this.Width = 1500;
            this.Height = 800;

            StartInfo si = new StartInfo();
            si.SetStockMarket(this);
            si.Text = "Start Information";
            si.MdiParent = this;
            si.Show();

            //change background colour to light pink
            foreach (Control ctrl in this.Controls)
            {

                if (ctrl is MdiClient)
                {
                    ctrl.BackColor = Color.LightPink;
                }

            }

        }

        public void SetValues(string name, string yourIP, string yourPort, string serverID, string serverPort)
        {
            this.label2.Text = name;
            this.clientName = name;
            this.label4.Text = yourIP;
            this.clientIP = yourIP;
            this.label10.Text = yourPort;
            this.clientPort = Int32.Parse(yourPort);
            this.label7.Text = serverID;
            this.serverIP = serverID;
            this.label9.Text = serverPort;
            this.serverPort = Int32.Parse(serverPort);
        }

    

        private void beginTradingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create three stocks and add them to the market
            Subject = new RealTimedata();
            Subject.clearCompanies();
            // In this lab assignment we will add three companies only using the following format:
            // Company symbol , Company name , Open price

            this.watchToolStripMenuItem.Visible = true;
            this.ordersToolStripMenuItem.Visible = true;
            this.beginTradingToolStripMenuItem.Enabled = false;
            this.stopTradingToolStripMenuItem.Enabled = true;
            this.marketToolStripMenuItem.Text = "&Join<<Connected>>";

            this.joinServer();
            this.ListCompanies();

            MarketDepthSubMenu(this.marketByOrderToolStripMenuItem1);
            MarketDepthSubMenu(this.marketByPriceToolStripMenuItem1);

            
            
            //var companies = JsonConvert.DeserializeObject(response);
            
            Console.WriteLine(response);
        }

        public void ListCompanies()
        {

            string listMessage = String.Format("LIST COMPANIES SME/TCP-1.0 \r\n " +
                                                        "CSEQ: {0} Session: {1}", ++CSeq, session);


            this.SMESend(listMessage);
            string retString = this.SMERecieve();
            Console.WriteLine("list companies received : {0}", retString);
            string ret = parse_server_response(retString);
            this.Subject.clearCompanies();
            JObject jsonData = JObject.Parse(ret);
            JArray json = (JArray)jsonData["companies"];
            foreach(JObject company in json)
            {
                //add the companies to the subject
                this.Subject.addCompany(company["symbol"].ToString(), company["name"].ToString(), Double.Parse(company["openPrice"].ToString()), Int32.Parse(company["volume"].ToString()), Double.Parse(company["lastPrice"].ToString()));
            }
        }

        private string parse_server_response(string retString)
        {
            //parse the server response to get the json object
            int i;
            for(i = 0; i < retString.Length; i++)
            {
                if(retString[i] == '{')
                {
                    break;
                }
            }
            string newstring = retString.Remove(0, i);
            return newstring;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //send the unregister command and shut down the socket
            string unregisterMessage = String.Format("UNREGISTER SME/TCP-1.0 \r\n" +
                                                            "ID: {0} CSeq: {1} Session: {2} ", this.clientName, ++CSeq, session);

            this.SMESend(unregisterMessage);
            string retString = this.SMERecieve();

            Console.WriteLine("unregister received: {0}", retString);
            mySocket.Shutdown(SocketShutdown.Both);
            mySocket.Close();

            this.Close();
        }

        private void StockStateSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockStateSummary summaryObserver = new StockStateSummary(Subject);
            summaryObserver.MdiParent = this;

            // Display the new form.
            summaryObserver.Show();
            response = String.Empty;

        }
        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cascade all MDI child windows.
            this.LayoutMdi(MdiLayout.Cascade);
        }



        private void arrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tile all child forms vertically.
            this.LayoutMdi(MdiLayout.ArrangeIcons);

        }

        private void horizontalTileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tile all child forms horizontally.
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void verticalTileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tile all child forms vertically.
            this.LayoutMdi(MdiLayout.TileVertical);

        }

        private void stopTradingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //send the unregister message and shutdown the socket
            string unregisterMessage = String.Format("UNREGISTER SME/TCP-1.0 \r\n" +
                                                            "ID: {0} CSeq: {1} Session: {2} ", this.clientName, ++CSeq, session);

            this.SMESend(unregisterMessage);
            string retString = this.SMERecieve();

            Console.WriteLine("unregister received: {0}", retString);

            // Release the socket.  
            mySocket.Shutdown(SocketShutdown.Both);
            mySocket.Close();

            this.Close();

        }



        public void MarketDepthSubMenu(ToolStripMenuItem MnuItems)
        {
            ToolStripMenuItem SSSMenu;
            List<Company> StockCompanies = Subject.getCompanies();
            foreach (Company company in StockCompanies)
            {
                if (MnuItems.Name == "marketByPriceToolStripMenuItem1")
                    SSSMenu = new ToolStripMenuItem(company.Name, null, marketByPriceToolStripMenuItem_Click);
                else
                    SSSMenu = new ToolStripMenuItem(company.Name, null, marketByOrderToolStripMenuItem_Click);
                MnuItems.DropDownItems.Add(SSSMenu);
            }
        }

        public void marketByOrderToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            //open the market by order and get the valid info
            var returnVal = this.GetBuyOrders();
            var stringjson = parse_server_response(returnVal);
            JObject jsonData = JObject.Parse(stringjson);

            JArray ja = (JArray)jsonData[sender.ToString()];
            Company cmp;

            //this is for all buy orders
            foreach (var company in this.Subject.getCompanies())
            {
                if (company.Name == sender.ToString())
                {
                    cmp = company;
                    cmp.clearBuyOrders();
                    foreach (JObject jo in ja)
                    {

                        cmp.addBuyOrder(Double.Parse(jo["price"].ToString()), Int32.Parse(jo["volume"].ToString()));
                    }
                }
            }

            var returnedSellVal = this.GetSellOrders();
            var sellStringjson = parse_server_response(returnedSellVal);
            JObject jsonSellData = JObject.Parse(sellStringjson);

            JArray jsa = (JArray)jsonSellData[sender.ToString()];

            //this is for all sell orders
            foreach (var c in this.Subject.getCompanies())
            {
                if (c.Name == sender.ToString())
                {
                    c.clearSellOrders();
                    foreach (JObject jo in jsa)
                    {
                        c.addSellOrder(Double.Parse(jo["price"].ToString()), Int32.Parse(jo["volume"].ToString()));
                    }
                }
            }

            MarketByOrder newMDIChild = new MarketByOrder(Subject, sender.ToString());
            // Set the parent form of the child window.
            newMDIChild.Text = "Market Depth By Order (" + sender.ToString() + ")";
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
            
        }
        private void marketByPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var returnVal = this.GetBuyOrders();
            var stringjson = parse_server_response(returnVal);
            JObject jsonData = JObject.Parse(stringjson);

            JArray ja = (JArray)jsonData[sender.ToString()];
            Company cmp;

            //this is for all buy orders
            foreach(var company in this.Subject.getCompanies())
            {
                if(company.Name == sender.ToString())
                {
                    cmp = company;
                    cmp.clearBuyOrders();
                    foreach (JObject jo in ja)
                    {
                        //add the companies buy orders
                        cmp.addBuyOrder(Double.Parse(jo["price"].ToString()), Int32.Parse(jo["volume"].ToString()));
                    }
                }
            }

            var returnedSellVal = this.GetSellOrders();
            var sellStringjson = parse_server_response(returnedSellVal);
            JObject jsonSellData = JObject.Parse(sellStringjson);

            JArray jsa = (JArray)jsonSellData[sender.ToString()];

            //this is for all sell orders
            foreach(var c in this.Subject.getCompanies())
            {
                if(c.Name == sender.ToString())
                {
                    c.clearSellOrders();
                    foreach(JObject jo in jsa)
                    {
                        //add the companies sell orders
                        c.addSellOrder(Double.Parse(jo["price"].ToString()), Int32.Parse(jo["volume"].ToString()));
                    }
                }
            }

           

            MarketByPrice newMDIChild = new MarketByPrice(Subject, sender.ToString());
            // Set the parent form of the child window.

            newMDIChild.Text = "Market Depth By Price (" + sender.ToString() + ")";

            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
            
        }

        private void bidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaceBidOrder newMDIChild = new PlaceBidOrder(Subject);
            newMDIChild.setStockMarket(this);
            // Set the parent form of the child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        public void sendBidOrder(Company selectedCompany, int price, int volume)
        {
            //send a bid order to the server
            var timestamp = DateTime.Now.ToString("h:mm:ss tt");
            var buyOrderForServer = new { symbol = selectedCompany.Symbol, volume = volume, price = price, timestamp = timestamp};
            string output = JsonConvert.SerializeObject(buyOrderForServer);

            string buyorderMessage = String.Format("BUYORDER SME/TCP-1.0 \r\n " +
                                                        "CSEQ: {0} Session: {1} Data {2}", ++CSeq, session, output);

            this.SMESend(buyorderMessage);
            string returnedString = this.SMERecieve();


            // Write the response to the console.  
            Console.WriteLine("Buy Order received : {0}", returnedString);

            //this.GetRecentInfo();
            
        }

        public void sendSellOrder(Company company, int price, int volume)
        {
            //send a sell order to the server
            var timestamp = DateTime.Now.ToString("h:mm:ss tt");
            var sellOrderForServer = new { symbol = company.Symbol, volume = volume, price = price, timestamp = timestamp };
            var output = JsonConvert.SerializeObject(sellOrderForServer);

            string sellOrderMessage = String.Format("SELLORDER SME/TCP-1.0 \r\n " +
                                                        "CSEQ: {0} Session: {1} Data: {2}", ++CSeq, session, output);

            this.SMESend(sellOrderMessage);
            var returnedString = this.SMERecieve();

            // Write the response to the console.  
            Console.WriteLine("Sell Order received : {0}", returnedString);

            //this.GetRecentInfo();
        }

        public void GetRecentInfo()
        {
            //command to get all recent info from server, notify causes this to run
            this.ListCompanies();
            var returnVal = this.GetBuyOrders();
            var stringjson = parse_server_response(returnVal);
            JObject jsonData = JObject.Parse(stringjson);

            //this is for all buy orders
            foreach (var company in this.Subject.getCompanies())
            {
                JArray ja = (JArray)jsonData[company.Name];
                Company cmp;
                
                    cmp = company;
                    cmp.clearBuyOrders();
                    foreach (JObject jo in ja)
                    {
                        cmp.addBuyOrder(Double.Parse(jo["price"].ToString()), Int32.Parse(jo["volume"].ToString()));
                    }
                
            }

            var returnedSellVal = this.GetSellOrders();
            var sellStringjson = parse_server_response(returnedSellVal);
            JObject jsonSellData = JObject.Parse(sellStringjson);

            //this is for all sell orders
            foreach (var c in this.Subject.getCompanies())
            {
                
                    JArray jsa = (JArray)jsonSellData[c.Name];
                    c.clearSellOrders();
                    foreach (JObject jo in jsa)
                    {
                        c.addSellOrder(Double.Parse(jo["price"].ToString()), Int32.Parse(jo["volume"].ToString()));
                    }
                
            }

            Console.WriteLine("\n ######################### Recent Info has been ran\n");
            

            Subject.Notify();
        }

        public string GetBuyOrders()
        {
            //get all buy orders from server
            string allBuyOrders = String.Format("LIST BUYORDERS SME/TCP-1.0" +
                                                "\n CSeq {0} Session: {1}", ++CSeq, session);
            Console.WriteLine("HERE");
            this.SMESend(allBuyOrders);
            var returnedString = this.SMERecieve();

            Console.WriteLine("All Buy Orders {0}", returnedString);
            return returnedString;
        }

        public string GetSellOrders()
        {
            //get all sell orders from server
            string allBuyOrders = String.Format("LIST SELLORDERS SME/TCP-1.0" +
                                                "\n CSeq {0} Session: {1}", ++CSeq, session);

            this.SMESend(allBuyOrders);
            var returnedString = this.SMERecieve();

            Console.WriteLine("All Sell Orders {0}", returnedString);

            return returnedString;

        }

        private void askToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaceSellOrder newMDIChild = new PlaceSellOrder(Subject);
            newMDIChild.SetStockSec(this);
            // Set the parent form of the child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private static ManualResetEvent connectDone =
       new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);
        private static ManualResetEvent infoPass =
            new ManualResetEvent(false);

        private static String response = String.Empty;

        // State object for receiving data from remote device.  
        public class StateObject
        {
            // Client socket.  
            public Socket workSocket = null;
            // Size of receive buffer.  
            public const int BufferSize = 256;
            // Receive buffer.  
            public byte[] buffer = new byte[BufferSize];
            // Received data string.  
            public StringBuilder sb = new StringBuilder();
        }

         private void joinServer()
        {
            // ManualResetEvent instances signal completion.  
       

            IPAddress bindAddress = IPAddress.Parse(this.serverIP);
            IPEndPoint bindEndPoint = new IPEndPoint(bindAddress, this.serverPort);
            
            try
            {
                mySocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

                mySocket.Connect(bindEndPoint);
                //register with the server
                string registerMessage = String.Format("REGISTER SME/TCP-1.0 \r\n " +
                                                        "ID: {0} CSEQ: {1}, Notification Port: {2}", this.clientName, ++CSeq, this.clientPort);

                this.SMESend(registerMessage);
                string returnedString = this.SMERecieve();

                Console.WriteLine("Register received: {0}", returnedString);
                string[] sa = returnedString.Split(' ');
                this.session = sa[6];
                Thread myTh = null;
                try {
                    myTh = new Thread(RunNotifySocket);
                    myTh.Start();
                }
                catch(Exception e)
                {
                    Console.WriteLine("Failure creating and running the thread" + e.Message);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }

        delegate void NotifyCallback();
        public void RunNotifySocket()
        {
            //this runs on a background thread
            IPAddress bindAddress = IPAddress.Parse(this.clientIP);
            IPEndPoint bindEndPoint = new IPEndPoint(bindAddress, this.clientPort);

            receiverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            receiverSocket.Bind(bindEndPoint);
            receiverSocket.Listen(int.MaxValue);

            Console.WriteLine("\n ########## \n Notify Socket Created");

            Socket tcpClient;


            tcpClient = receiverSocket.Accept();
            byte[] buffer = new byte[2048];

            while (true)
            {
                try
                {
                    Array.Clear(buffer, 0, buffer.Length);
                    int byteRecv = tcpClient.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                    string receviedData = Encoding.ASCII.GetString(buffer, 0, byteRecv);
                    if (receviedData.Contains("BYE"))
                    {
                        break;
                    }
                    Console.WriteLine("\n ################# \n received {0}", receviedData);
                    //post to the main ui thread to run the get recent info function
                    NotifyCallback d = new NotifyCallback(GetRecentInfo);
                    this.Invoke(d);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Didnt work" + e.Message);

                }
            }

            receiverSocket.Close();
            
        }

        public void SMESend(string data)
        {
            try
            {
                //send message
                byte[] byteData = Encoding.ASCII.GetBytes(data);
                mySocket.Send(byteData, 0, byteData.Length, SocketFlags.None);
            }
            catch (Exception e)
            {
                throw new Exception("SMEsocket.Send" + e.Message);
            }
        }

        public string SMERecieve()
        {
            try
            {
                //receive message
                byte[] buffer = new byte[2048];
                int byteRecv = mySocket.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                string receviedData = Encoding.ASCII.GetString(buffer, 0, byteRecv);

                return receviedData;

            }
            catch(Exception e)
            {
                return null;
            }
        }

    }
}
