namespace DatabaseManager
{
    partial class PlayerMonster
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.HeadPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.DataGirdView = new System.Windows.Forms.DataGridView();
            this.BodyPanel = new System.Windows.Forms.Panel();
            this.HeadPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGirdView)).BeginInit();
            this.BodyPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // HeadPanel
            // 
            this.HeadPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(244)))), ((int)(((byte)(222)))));
            this.HeadPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HeadPanel.Controls.Add(this.label1);
            this.HeadPanel.Controls.Add(this.comboBox1);
            this.HeadPanel.Location = new System.Drawing.Point(5, 5);
            this.HeadPanel.Name = "HeadPanel";
            this.HeadPanel.Size = new System.Drawing.Size(947, 172);
            this.HeadPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Book Antiqua", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "SearchMethod:";
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(144)))), ((int)(((byte)(122)))));
            this.comboBox1.Font = new System.Drawing.Font("Book Antiqua", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.ForeColor = System.Drawing.Color.Transparent;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "PlayerID",
            "PlayerMonsterID"});
            this.comboBox1.Location = new System.Drawing.Point(162, 8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(148, 31);
            this.comboBox1.TabIndex = 1;
            // 
            // DataGirdView
            // 
            this.DataGirdView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(244)))), ((int)(((byte)(222)))));
            this.DataGirdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGirdView.Location = new System.Drawing.Point(5, 5);
            this.DataGirdView.Name = "DataGirdView";
            this.DataGirdView.RowTemplate.Height = 24;
            this.DataGirdView.Size = new System.Drawing.Size(935, 445);
            this.DataGirdView.TabIndex = 0;
            // 
            // BodyPanel
            // 
            this.BodyPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BodyPanel.Controls.Add(this.DataGirdView);
            this.BodyPanel.Location = new System.Drawing.Point(5, 185);
            this.BodyPanel.Name = "BodyPanel";
            this.BodyPanel.Size = new System.Drawing.Size(945, 455);
            this.BodyPanel.TabIndex = 1;
            // 
            // PlayerMonster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BodyPanel);
            this.Controls.Add(this.HeadPanel);
            this.Name = "PlayerMonster";
            this.Size = new System.Drawing.Size(956, 649);
            this.HeadPanel.ResumeLayout(false);
            this.HeadPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGirdView)).EndInit();
            this.BodyPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel HeadPanel;
        private System.Windows.Forms.DataGridView DataGirdView;
        private System.Windows.Forms.Panel BodyPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}
