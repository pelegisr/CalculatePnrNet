
namespace TestCalculatePnrNet
{
    partial class Form1
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
            this.uPanel = new Peleg.UPanelInfo.UPanel();
            this.txtPnr = new System.Windows.Forms.TextBox();
            this.btnCalculateGross = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // uPanel
            // 
            this.uPanel.ChangeParentIcon = true;
            this.uPanel.ChangeSystemLogo = true;
            this.uPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.uPanel.Location = new System.Drawing.Point(0, 0);
            this.uPanel.MinimumSize = new System.Drawing.Size(642, 52);
            this.uPanel.Mode = "";
            this.uPanel.Name = "uPanel";
            this.uPanel.Size = new System.Drawing.Size(642, 52);
            this.uPanel.TabIndex = 0;
            this.uPanel.TranslateParent = false;
            this.uPanel.WindowLess = false;
            // 
            // txtPnr
            // 
            this.txtPnr.Location = new System.Drawing.Point(77, 85);
            this.txtPnr.Name = "txtPnr";
            this.txtPnr.Size = new System.Drawing.Size(131, 20);
            this.txtPnr.TabIndex = 1;
            this.txtPnr.Text = "305752";
            // 
            // btnCalculateGross
            // 
            this.btnCalculateGross.Location = new System.Drawing.Point(214, 85);
            this.btnCalculateGross.Name = "btnCalculateGross";
            this.btnCalculateGross.Size = new System.Drawing.Size(104, 20);
            this.btnCalculateGross.TabIndex = 2;
            this.btnCalculateGross.Text = "Calculate Gross";
            this.btnCalculateGross.UseVisualStyleBackColor = true;
            this.btnCalculateGross.Click += new System.EventHandler(this.btnCalculateGross_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(74, 122);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(47, 13);
            this.lblResult.TabIndex = 3;
            this.lblResult.Text = "lblResult";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(74, 146);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(60, 13);
            this.lblMessage.TabIndex = 4;
            this.lblMessage.Text = "lblMessage";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnCalculateGross);
            this.Controls.Add(this.txtPnr);
            this.Controls.Add(this.uPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Peleg.UPanelInfo.UPanel uPanel;
        private System.Windows.Forms.TextBox txtPnr;
        private System.Windows.Forms.Button btnCalculateGross;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblMessage;
    }
}

