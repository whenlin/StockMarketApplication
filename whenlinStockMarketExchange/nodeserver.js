var net = require('net');
var HOST = '127.0.0.1';
var PORT = 3000;
var server = net.createServer();
var SSeq = 0
server.timeout = 0;
server.listen(PORT, HOST);

class Company {
	constructor(symbol, name, openPrice) {
		this.symbol = symbol;
		this.name = name;
		this.openPrice = openPrice;
		this.lastPrice = openPrice;
		this.volume = 0;
		this.buyOrders = [];
		this.sellOrders = [];
	}

	setLastPrice(lastprice) {
		this.lastPrice = lastprice;
	}

	getLastPrice(lastprice) {
		return this.lastprice;
	}

	getVolume() {
		return this.volume;
	}

	addBuyOrder(buyPrice, buySize) {
		if(this.sellOrders.length > 0) {
			for(var i = 0; i < this.sellOrders.length; i++) {
				var curr = this.sellOrders[i];
				if(buyPrice == curr.price) {
					if(curr.volume > buySize) {
						//sell order is bigger than the incoming buy size
						curr.volume = curr.volume - buySize;
						this.lastPrice = buyPrice;
						this.volume = this.volume + buySize;
						return;
					}
					else if(buySize > curr.volume) {
						//buy order is bigger than the sell order size
						buySize = buySize - curr.volume;
						this.lastPrice = buyPrice;
						this.volume = this.volume + curr.volume;
						this.sellOrders.splice(i, 1);
						this.buyOrders.push(new BuyOrder(buyPrice, buySize));
						return;
					}
					else {
						//they are equal
						this.lastPrice = buyPrice;
						this.volume = this.volume + curr.volume;
						this.sellOrders.splice(i, 1);
						return;
					}
				}
					
				
			}
			//fell through the for loop
			this.buyOrders.push(new BuyOrder(buyPrice, buySize));
			return;
		}

		else {
			this.buyOrders.push(new BuyOrder(buyPrice, buySize));
		}
	}

	addSellOrder(sellPrice, sellSize) {
		if(this.buyOrders.length > 0) {
			for(var i = 0; i < this.buyOrders.length; i++) {
				var curr = this.buyOrders[i];
				if(sellPrice == curr.price) {
					if(curr.volume > sellSize) {
						//buy order is bigger than incoming sell order
						curr.volume = curr.volume - sellSize;
						this.lastPrice = sellPrice;
						this.volume = this.volume + sellSize;
						return;
					}
					else if(sellSize > curr.volume) {
						//sell order is bigger than found buy order
						sellSize = sellSize - curr.volume;
						this.lastPrice = sellPrice;
						this.volume = this.volume + curr.volume;
						this.buyOrders.splice(i, 1);
						this.sellOrders.push(new SellOrder(sellPrice, sellSize));
						return;
					}

					else {
						//they are equal
						this.lastPrice = sellPrice;
						this.volume = this.volume + curr.volume;
						this.buyOrders.splice(i, 1);
						return;
					}
				}
			}
			//fell through the for loop
			this.sellOrders.push(new SellOrder(sellPrice, sellSize));
			return;
		}
		else {
			this.sellOrders.push(new SellOrder(sellPrice, sellSize));
		}
	}
}

class Order {
	constructor(price, volume) {
		this.price = price;
		this.volume = volume;
		this.time = new Date();
	}
}

class BuyOrder extends Order {
	
}

class SellOrder extends Order {

}



class RealTimeData {
	constructor() {
		this.tradingDate = new Date();
		this._companies = [];
	}
}

var rtd = new RealTimeData();
var msft = new Company('MSFT', 'Microsoft', 46.13);
var aapl = new Company('AAPL', 'Apple Inc.', 105.22);
var fb = new Company('FB', 'Facebook Inc', 80.67);

rtd._companies.push(msft);
rtd._companies.push(aapl);
rtd._companies.push(fb);



console.log('Server listening on ' + HOST +':'+ PORT);
server.on('connection', function(sock) {
		console.log('CONNECTED: ' + sock.remoteAddress +':'+ sock.remotePort);
		sock.on('data', readRespond);

		sock.on('close', function(data) {
			console.log('CLOSED: ' + sock.remoteAddress +' '+ sock.remotePort);
		});

function GenRandomNumber() {
	return Math.round(Math.random() * 2000);
}

var sessionID =  GenRandomNumber();
var allClients = [];
var client;	
function readRespond(data) {
		console.log('DATA ' + sock.remoteAddress + ': ' + data);
		var myretstring = data.toString('ascii').split(' ');
		
		if (data.includes('UNREGISTER')) {
			var cseq = myretstring[5];
			sock.write(`SME/TCP-1.0 OK \n CSeq: ${cseq} Session: ${sessionID} \r\n`);
			client.write('BYE');
			sock.destroy();
		}
		else if(data.includes('REGISTER')) {
			var cseq = myretstring[4];
			console.log("in register", myretstring[9])
			sock.write(`SME/TCP-1.0 OK \n CSeq: ${cseq} Session: ${sessionID} \r\n`);
			client = new net.Socket();
			allClients.push(client);
			client.connect(Number(myretstring[9]), sock.remoteAddress, function() {
				console.log('NOTIFY SOCKET OPENED');
				//send update command
				client.write(`NOTIFY SME/TCP-1.0 \n SSeq: ${SSeq++}`);
			});

			client.on('data', function(data) {
				console.log('DATA: ' + data);
				//if sent data, must be to close the client
				client.destroy();
			})

			client.on('close', function() {
				console.log("Connection close");
			})
		}
		 
		else if(data.includes('LIST COMPANIES')) {
			var cseq = myretstring[5];
			var output = JSON.stringify(rtd._companies);
			sock.write(`SME/TCP-1.0 OK \n CSeq: ${cseq} Session: ${sessionID} Data: {"companies": ${output}}\r\n`)
		}
		else if(data.includes('LIST SELLORDERS')) {
			var cseq = myretstring[5];
			var so = {};
			for(var i = 0; i < rtd._companies.length; i++) {
				so[rtd._companies[i].name] = rtd._companies[i].sellOrders;
			}
			sock.write(`SME/TCP-1.0 OK CSeq: ${cseq} Session: ${sessionID} Data: ${JSON.stringify(so)}`);
		}
		else if (data.includes("LIST BUYORDERS")){
			var cseq = myretstring[5];
			var so = {};
			for(var i = 0; i < rtd._companies.length; i++) {
				so[rtd._companies[i].name] = rtd._companies[i].buyOrders;
			}
			sock.write(`SME/TCP-1.0 OK CSeq: ${cseq} Session: ${sessionID} Data: ${JSON.stringify(so)}`);
		}
		else if(data.includes('BUYORDER')) {
			var cseq = myretstring[4];
			sock.write(`SME/TCP-1.0 OK \n CSeq: ${cseq} Session: ${sessionID} \r\n`);
			var stringData = data.toString('ascii')
			var i;
			for(i = 0; i < stringData.length; i++) {
				if(stringData[i] == '{') {
					break;
				}
			}
			var json = stringData.substring(i);
			
			var obj = JSON.parse(json);
			for(var a = 0; a < rtd._companies.length; a++){
				if(rtd._companies[a].symbol == obj.symbol) {
					rtd._companies[a].addBuyOrder(obj.price, obj.volume);
				}
			}
			for(var i = 0; i < allClients.length; i++){
				//update observers
				allClients[i].write(`NOTIFY SME/TCP-1.0 \n SSeq: ${SSeq}`);
			}

		}
		else if(data.includes('SELLORDER')) {
			var cseq = myretstring[4];
			sock.write(`SME/TCP-1.0 OK \n CSeq: ${cseq} Session: ${sessionID} \r\n`);
			var stringData = data.toString('ascii');
			var i;
			for(i = 0; i < stringData.length; i++) {
				if(stringData[i] == '{') {
					break;
				}
			}
			var json = stringData.substring(i);
			var obj = JSON.parse(json);

			for(var a = 0; a < rtd._companies.length; a++) {
				if(rtd._companies[a].symbol == obj.symbol)  {
					//add the sell order to the company
				  rtd._companies[a].addSellOrder(obj.price, obj.volume);
				}
			}

			for(var i = 0; i < allClients.length; i++){
				//new sell order so update observers
				allClients[i].write(`NOTIFY SME/TCP-1.0 \n SSeq: ${SSeq}`);
			}
		}
		
		}
});

