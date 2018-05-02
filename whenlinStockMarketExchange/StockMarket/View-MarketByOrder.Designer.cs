namespace StockExchangeMarket
{
    partial class MarketByOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarketByOrder));
            this.ordersGrid = new System.Windows.Forms.DataGridView();
            this.buyVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buyPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sellVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Buy = new System.Windows.Forms.Label();
            this.Sell = new System.Windows.Forms.Label();
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
            this.buyVolume,
            this.buyPrice,
            this.salePrice,
            this.sellVolume});
            this.ordersGrid.Location = new System.Drawing.Point(12, 35);
            this.ordersGrid.Name = "ordersGrid";
            this.ordersGrid.ReadOnly = true;
            this.ordersGrid.Size = new System.Drawing.Size(363, 223);
            this.ordersGrid.TabIndex = 1;
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
            this.buyPrice.Width = 80;
            // 
            // salePrice
            // 
            this.salePrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.salePrice.HeaderText = "Price";
            this.salePrice.Name = "salePrice";
            this.salePrice.ReadOnly = true;
            this.salePrice.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.salePrice.Width = 80;
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
            // Buy
            // 
            this.Buy.AutoSize = true;
            this.Buy.Location = new System.Drawing.Point(115, 19);
            this.Buy.Name = "Buy";
            this.Buy.Size = new System.Drawing.Size(25, 13);
            this.Buy.TabIndex = 2;
            this.Buy.Text = "Buy";
            // 
            // Sell
            // 
            this.Sell.AutoSize = true;
            this.Sell.Location = new System.Drawing.Point(267, 19);
            this.Sell.Name = "Sell";
            this.Sell.Size = new System.Drawing.Size(24, 13);
            this.Sell.TabIndex = 3;
            this.Sell.Text = "Sell";
            // 
            // MarketByOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 277);
            this.Controls.Add(this.Sell);
            this.Controls.Add(this.Buy);
            this.Controls.Add(this.ordersGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MarketByOrder";
            this.Text = "View_MarketByOrder";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MarketByOrder_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.ordersGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView ordersGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn buyVolume;
        private System.Windows.Forms.DataGridViewTextBoxColumn buyPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn salePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn sellVolume;
        private System.Windows.Forms.Label Buy;
        private System.Windows.Forms.Label Sell;
    }
}