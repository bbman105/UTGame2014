using System.IO;
using System.Text;
using System;
using System.Windows.Forms;
namespace DatabaseManager
{
    partial class OnlinePlayer
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
            this.Label_Title_PLayerNumber = new System.Windows.Forms.Label();
            this.Label_PlayerNumber = new System.Windows.Forms.Label();
            this.HeadPane = new System.Windows.Forms.Panel();
            this.RichBox = new System.Windows.Forms.RichTextBox();
            this.HeadPane.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label_Title_PLayerNumber
            // 
            this.Label_Title_PLayerNumber.AutoSize = true;
            this.Label_Title_PLayerNumber.Font = new System.Drawing.Font("Book Antiqua", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Title_PLayerNumber.Location = new System.Drawing.Point(10, 10);
            this.Label_Title_PLayerNumber.Name = "Label_Title_PLayerNumber";
            this.Label_Title_PLayerNumber.Size = new System.Drawing.Size(248, 28);
            this.Label_Title_PLayerNumber.TabIndex = 0;
            this.Label_Title_PLayerNumber.Text = "OnlinePlayerNumber:";
            // 
            // Label_PlayerNumber
            // 
            this.Label_PlayerNumber.AutoSize = true;
            this.Label_PlayerNumber.Font = new System.Drawing.Font("Book Antiqua", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_PlayerNumber.Location = new System.Drawing.Point(257, 10);
            this.Label_PlayerNumber.Name = "Label_PlayerNumber";
            this.Label_PlayerNumber.Size = new System.Drawing.Size(24, 28);
            this.Label_PlayerNumber.TabIndex = 1;
            this.Label_PlayerNumber.Text = "0";
            // 
            // HeadPane
            // 
            this.HeadPane.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(244)))), ((int)(((byte)(222)))));
            this.HeadPane.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HeadPane.Controls.Add(this.Label_Title_PLayerNumber);
            this.HeadPane.Controls.Add(this.Label_PlayerNumber);
            this.HeadPane.Location = new System.Drawing.Point(5, 5);
            this.HeadPane.Name = "HeadPane";
            this.HeadPane.Size = new System.Drawing.Size(947, 172);
            this.HeadPane.TabIndex = 2;
            // 
            // RichBox
            // 
            this.RichBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(244)))), ((int)(((byte)(222)))));
            this.RichBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RichBox.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RichBox.Location = new System.Drawing.Point(5, 185);
            this.RichBox.Name = "RichBox";
            this.RichBox.ReadOnly = true;
            this.RichBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.RichBox.Size = new System.Drawing.Size(945, 455);
            this.RichBox.TabIndex = 0;
            this.RichBox.Text = "玩家scozirge登入-----------2014/11/10/16/52------------";
            // 
            // OnlinePlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RichBox);
            this.Controls.Add(this.HeadPane);
            this.Name = "OnlinePlayer";
            this.Size = new System.Drawing.Size(956, 649);
            this.HeadPane.ResumeLayout(false);
            this.HeadPane.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label Label_Title_PLayerNumber;
        public System.Windows.Forms.Label Label_PlayerNumber;
        public System.Windows.Forms.Panel HeadPane;
        public System.Windows.Forms.RichTextBox RichBox;
    }
}
