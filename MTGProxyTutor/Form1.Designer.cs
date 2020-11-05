namespace MTGProxyTutor
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
			this.cardList = new System.Windows.Forms.TextBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.searchCardsBtn = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.listView1 = new System.Windows.Forms.ListView();
			this.CardQuantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.CardName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.CardRarity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ExportToPDFBtn = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cardList
			// 
			this.cardList.Location = new System.Drawing.Point(12, 71);
			this.cardList.Multiline = true;
			this.cardList.Name = "cardList";
			this.cardList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.cardList.Size = new System.Drawing.Size(303, 470);
			this.cardList.TabIndex = 0;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(123, 560);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(192, 33);
			this.progressBar1.TabIndex = 1;
			this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
			// 
			// searchCardsBtn
			// 
			this.searchCardsBtn.Location = new System.Drawing.Point(12, 560);
			this.searchCardsBtn.Name = "searchCardsBtn";
			this.searchCardsBtn.Size = new System.Drawing.Size(94, 33);
			this.searchCardsBtn.TabIndex = 2;
			this.searchCardsBtn.Text = "Search";
			this.searchCardsBtn.UseVisualStyleBackColor = true;
			this.searchCardsBtn.Click += new System.EventHandler(this.button1_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonShadow;
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(805, 28);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
			this.aboutToolStripMenuItem.Text = "About";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 47);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(126, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "Paste the list here:";
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CardName,
            this.CardQuantity,
            this.CardRarity});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(334, 71);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(459, 470);
			this.listView1.TabIndex = 5;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// CardQuantity
			// 
			this.CardQuantity.Text = "Quantity";
			// 
			// CardName
			// 
			this.CardName.Text = "Name";
			this.CardName.Width = 260;
			// 
			// CardRarity
			// 
			this.CardRarity.Text = "Rarity";
			// 
			// ExportToPDFBtn
			// 
			this.ExportToPDFBtn.Location = new System.Drawing.Point(682, 560);
			this.ExportToPDFBtn.Name = "ExportToPDFBtn";
			this.ExportToPDFBtn.Size = new System.Drawing.Size(111, 33);
			this.ExportToPDFBtn.TabIndex = 6;
			this.ExportToPDFBtn.Text = "Export to PDF";
			this.ExportToPDFBtn.UseVisualStyleBackColor = true;
			this.ExportToPDFBtn.Click += new System.EventHandler(this.ExportToPDFBtn_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(805, 605);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.ExportToPDFBtn);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.searchCardsBtn);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.cardList);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "MTG Proxy Tutor";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox cardList;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Button searchCardsBtn;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader CardQuantity;
		private System.Windows.Forms.ColumnHeader CardName;
		private System.Windows.Forms.ColumnHeader CardRarity;
		private System.Windows.Forms.Button ExportToPDFBtn;
	}
}

