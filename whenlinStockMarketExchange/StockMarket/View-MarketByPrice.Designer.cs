namespace StockExchangeMarket
{
    partial class MarketByPrice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarketByPrice));
            this.ordersGrid = new System.Windows.Forms.DataGridView();
            this.sellNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buyVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buyPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sellVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buyNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sell = new System.Windows.Forms.Label();
            this.Buy = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ordersGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ordersGrid
            // 
            this.ordersGrid.AllowDrop = true;
            this.ordersGrid.AllowUserToAddRows = false;
            this.ordersGrid.AllowUserToDeleteRows = false;
            this.ordersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ordersGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ordersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ordersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sellNo,
            this.buyVolume,
            this.buyPrice,
            this.salePrice,
            this.sellVolume,
            this.buyNo});
            this.ordersGrid.Location = new System.Drawing.Point(12, 29);
            this.ordersGrid.Name = "ordersGrid";
            this.ordersGrid.ReadOnly = true;
            this.ordersGrid.Size = new System.Drawing.Size(404, 224);
            this.ordersGrid.TabIndex = 2;
            // 
            // sellNo
            // 
            this.sellNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sellNo.HeaderText = "#";
            this.sellNo.Name = "sellNo";
            this.sellNo.ReadOnly = true;
            this.sellNo.Width = 30;
            // 
            // buyVolume
            // 
            this.buyVolume.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = null;
            this.buyVolume.DefaultCellStyle = dataGridViewCellStyle1;
            this.buyVolume.HeaderText = "Volume";
            this.buyVolume.Name = "buyVolume";
            this.buyVolume.ReadOnly = true;
            this.buyVolume.Width = 80;
            // 
            // buyPrice
            // 
            this.buyPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.buyPrice.DefaultCellStyle = dataGridViewCellStyle2;
            this.buyPrice.FillWeight = 70F;
            this.buyPrice.HeaderText = "Price";
            this.buyPrice.Name = "buyPrice";
            this.buyPrice.ReadOnly = true;
            this.buyPrice.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.buyPrice.Width = 70;
            // 
            // salePrice
            // 
            this.salePrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.salePrice.HeaderText = "Price";
            this.salePrice.Name = "salePrice";
            this.salePrice.ReadOnly = true;
            this.salePrice.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.salePrice.Width = 70;
            // 
            // sellVolume
            // 
            this.sellVolume.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sellVolume.HeaderText = "Volume";
            this.sellVolume.Name = "sellVolume";
            this.sellVolume.ReadOnly = true;
            this.sellVolume.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.sellVolume.Width = 80;
            // 
            // buyNo
            // 
            this.buyNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.buyNo.HeaderText = "#";
            this.buyNo.Name = "buyNo";
            this.buyNo.ReadOnly = true;
            this.buyNo.Width = 30;
            // 
            // Sell
            // 
            this.Sell.AutoSize = true;
            this.Sell.Location = new System.Drawing.Point(289, 13);
            this.Sell.Name = "Sell";
            this.Sell.Size = new System.Drawing.Size(24, 13);
            this.Sell.TabIndex = 5;
            this.Sell.Text = "Sell";
            // 
            // Buy
            // 
            this.Buy.AutoSize = true;
            this.Buy.Location = new System.Drawing.Point(132, 13);
            this.Buy.Name = "Buy";
            this.Buy.Size = new System.Drawing.Size(25, 13);
            this.Buy.TabIndex = 4;
            this.Buy.Text = "Buy";
            // 
            // MarketByPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 284);
            this.Controls.Add(this.Sell);
            this.Controls.Add(this.Buy);
            this.Controls.Add(this.ordersGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MarketByPrice";
            this.Text = "Market Depth By Price";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MarketByPrice_FormClosed);
            this.Load += new System.EventHandler(this.MarketByPrice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ordersGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView ordersGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn sellNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn buyVolume;
        private System.Windows.Forms.DataGridViewTextBoxColumn buyPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn salePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn sellVolume;
        private System.Windows.Forms.DataGridViewTextBoxColumn buyNo;
        private System.Windows.Forms.Label Sell;
        private System.Windows.Forms.Label Buy;
    }
}