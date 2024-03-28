namespace Connectron
{
	partial class Game
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
			this.btnEnd = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.pnlScores = new System.Windows.Forms.Panel();
			this.pnlGrid = new System.Windows.Forms.FlowLayoutPanel();
			this.tblGrid = new System.Windows.Forms.DataGridView();
			this.btnBomb = new System.Windows.Forms.Button();
			this.btnUndo = new System.Windows.Forms.Button();
			this.pnlGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tblGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// btnEnd
			// 
			this.btnEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEnd.Enabled = false;
			this.btnEnd.Location = new System.Drawing.Point(1243, 48);
			this.btnEnd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnEnd.Name = "btnEnd";
			this.btnEnd.Size = new System.Drawing.Size(149, 51);
			this.btnEnd.TabIndex = 29;
			this.btnEnd.Text = "End Game";
			this.btnEnd.UseVisualStyleBackColor = true;
			this.btnEnd.Click += new System.EventHandler(this.BtnEnd_Click);
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStart.Location = new System.Drawing.Point(1060, 48);
			this.btnStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(149, 51);
			this.btnStart.TabIndex = 28;
			this.btnStart.Text = "Start Game";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
			// 
			// pnlScores
			// 
			this.pnlScores.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlScores.BackColor = System.Drawing.Color.Transparent;
			this.pnlScores.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlScores.Location = new System.Drawing.Point(863, 160);
			this.pnlScores.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.pnlScores.Name = "pnlScores";
			this.pnlScores.Size = new System.Drawing.Size(580, 848);
			this.pnlScores.TabIndex = 27;
			// 
			// pnlGrid
			// 
			this.pnlGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlGrid.BackColor = System.Drawing.Color.Transparent;
			this.pnlGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlGrid.Controls.Add(this.tblGrid);
			this.pnlGrid.Location = new System.Drawing.Point(15, 14);
			this.pnlGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.pnlGrid.Name = "pnlGrid";
			this.pnlGrid.Size = new System.Drawing.Size(799, 800);
			this.pnlGrid.TabIndex = 16;
			// 
			// tblGrid
			// 
			this.tblGrid.AllowUserToAddRows = false;
			this.tblGrid.AllowUserToDeleteRows = false;
			this.tblGrid.AllowUserToResizeColumns = false;
			this.tblGrid.AllowUserToResizeRows = false;
			this.tblGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.tblGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.tblGrid.ColumnHeadersVisible = false;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.tblGrid.DefaultCellStyle = dataGridViewCellStyle1;
			this.tblGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.tblGrid.Enabled = false;
			this.tblGrid.Location = new System.Drawing.Point(3, 2);
			this.tblGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tblGrid.Name = "tblGrid";
			this.tblGrid.ReadOnly = true;
			this.tblGrid.RowHeadersVisible = false;
			this.tblGrid.RowTemplate.Height = 33;
			this.tblGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.tblGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.tblGrid.Size = new System.Drawing.Size(240, 150);
			this.tblGrid.TabIndex = 0;
			this.tblGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TblGrid_CellClick);
			this.tblGrid.SelectionChanged += new System.EventHandler(this.TblGrid_SelectionChanged);
			// 
			// btnBomb
			// 
			this.btnBomb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBomb.BackColor = System.Drawing.Color.LightGray;
			this.btnBomb.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBomb.BackgroundImage")));
			this.btnBomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnBomb.Enabled = false;
			this.btnBomb.Location = new System.Drawing.Point(953, 35);
			this.btnBomb.Margin = new System.Windows.Forms.Padding(4);
			this.btnBomb.Name = "btnBomb";
			this.btnBomb.Size = new System.Drawing.Size(84, 76);
			this.btnBomb.TabIndex = 30;
			this.btnBomb.UseVisualStyleBackColor = false;
			this.btnBomb.Click += new System.EventHandler(this.BtnBomb_Click);
			// 
			// btnUndo
			// 
			this.btnUndo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUndo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUndo.BackgroundImage")));
			this.btnUndo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnUndo.Enabled = false;
			this.btnUndo.Location = new System.Drawing.Point(849, 35);
			this.btnUndo.Margin = new System.Windows.Forms.Padding(4);
			this.btnUndo.Name = "btnUndo";
			this.btnUndo.Size = new System.Drawing.Size(84, 76);
			this.btnUndo.TabIndex = 31;
			this.btnUndo.UseVisualStyleBackColor = true;
			this.btnUndo.Click += new System.EventHandler(this.BtnUndo_Click);
			// 
			// Game
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1453, 1029);
			this.Controls.Add(this.btnUndo);
			this.Controls.Add(this.btnBomb);
			this.Controls.Add(this.btnEnd);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.pnlScores);
			this.Controls.Add(this.pnlGrid);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Game";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Connectron";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.Game_Load);
			this.SizeChanged += new System.EventHandler(this.Game_SizeChanged);
			this.pnlGrid.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tblGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnEnd;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Panel pnlScores;
		private System.Windows.Forms.FlowLayoutPanel pnlGrid;
		private System.Windows.Forms.DataGridView tblGrid;
		private System.Windows.Forms.Button btnBomb;
		private System.Windows.Forms.Button btnUndo;
	}
}