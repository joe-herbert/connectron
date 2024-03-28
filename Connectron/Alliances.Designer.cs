namespace Connectron
{
	partial class Alliances
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
			this.pnlAlliances = new System.Windows.Forms.FlowLayoutPanel();
			this.btnDone = new System.Windows.Forms.Button();
			this.btnAddAlliance = new System.Windows.Forms.Button();
			this.lblTitle = new System.Windows.Forms.Label();
			this.cmbAddAlliance = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// pnlAlliances
			// 
			this.pnlAlliances.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlAlliances.Location = new System.Drawing.Point(12, 233);
			this.pnlAlliances.Name = "pnlAlliances";
			this.pnlAlliances.Size = new System.Drawing.Size(878, 747);
			this.pnlAlliances.TabIndex = 8;
			// 
			// btnDone
			// 
			this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDone.Location = new System.Drawing.Point(723, 157);
			this.btnDone.Name = "btnDone";
			this.btnDone.Size = new System.Drawing.Size(167, 62);
			this.btnDone.TabIndex = 7;
			this.btnDone.Text = "Done";
			this.btnDone.UseVisualStyleBackColor = true;
			this.btnDone.Visible = false;
			this.btnDone.Click += new System.EventHandler(this.BtnDone_Click);
			// 
			// btnAddAlliance
			// 
			this.btnAddAlliance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddAlliance.Location = new System.Drawing.Point(23, 160);
			this.btnAddAlliance.Name = "btnAddAlliance";
			this.btnAddAlliance.Size = new System.Drawing.Size(167, 56);
			this.btnAddAlliance.TabIndex = 6;
			this.btnAddAlliance.Text = "Add Alliance";
			this.btnAddAlliance.UseVisualStyleBackColor = true;
			this.btnAddAlliance.Click += new System.EventHandler(this.BtnAddAlliance_Click);
			// 
			// lblTitle
			// 
			this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
			this.lblTitle.Location = new System.Drawing.Point(12, 51);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(878, 67);
			this.lblTitle.TabIndex = 5;
			this.lblTitle.Text = "Alliances";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cmbAddAlliance
			// 
			this.cmbAddAlliance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbAddAlliance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbAddAlliance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
			this.cmbAddAlliance.FormattingEnabled = true;
			this.cmbAddAlliance.Location = new System.Drawing.Point(217, 163);
			this.cmbAddAlliance.Name = "cmbAddAlliance";
			this.cmbAddAlliance.Size = new System.Drawing.Size(476, 50);
			this.cmbAddAlliance.TabIndex = 9;
			this.cmbAddAlliance.Visible = false;
			// 
			// Alliances
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(902, 992);
			this.Controls.Add(this.cmbAddAlliance);
			this.Controls.Add(this.pnlAlliances);
			this.Controls.Add(this.btnDone);
			this.Controls.Add(this.btnAddAlliance);
			this.Controls.Add(this.lblTitle);
			this.MinimumSize = new System.Drawing.Size(928, 1033);
			this.Name = "Alliances";
			this.Text = "Alliances";
			this.Load += new System.EventHandler(this.Alliances_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel pnlAlliances;
		private System.Windows.Forms.Button btnDone;
		private System.Windows.Forms.Button btnAddAlliance;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.ComboBox cmbAddAlliance;
	}
}