namespace WizWar1 {
	partial class CardPreview {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.cardText = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cardTitle = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// cardText
			// 
			this.cardText.BackColor = System.Drawing.SystemColors.Info;
			this.cardText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.cardText.Enabled = false;
			this.cardText.Font = new System.Drawing.Font("Buxton Sketch", 12.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cardText.Location = new System.Drawing.Point(5, 10);
			this.cardText.Margin = new System.Windows.Forms.Padding(39);
			this.cardText.Multiline = true;
			this.cardText.Name = "cardText";
			this.cardText.ReadOnly = true;
			this.cardText.Size = new System.Drawing.Size(162, 243);
			this.cardText.TabIndex = 1;
			this.cardText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.cardText.TextChanged += new System.EventHandler(this.cardPreview_TextChanged);
			// 
			// panel1
			// 
			this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.panel1.BackColor = System.Drawing.SystemColors.Info;
			this.panel1.Controls.Add(this.cardTitle);
			this.panel1.Controls.Add(this.cardText);
			this.panel1.Location = new System.Drawing.Point(39, 41);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(10, 15, 10, 10);
			this.panel1.Size = new System.Drawing.Size(172, 273);
			this.panel1.TabIndex = 2;
			// 
			// cardTitle
			// 
			this.cardTitle.BackColor = System.Drawing.Color.Transparent;
			this.cardTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.cardTitle.Font = new System.Drawing.Font("Buxton Sketch", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cardTitle.Location = new System.Drawing.Point(10, 15);
			this.cardTitle.Name = "cardTitle";
			this.cardTitle.Size = new System.Drawing.Size(152, 33);
			this.cardTitle.TabIndex = 2;
			this.cardTitle.Text = "(card title)";
			this.cardTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.Image = global::WizWar1.Properties.Resources.card_border1;
			this.pictureBox1.InitialImage = global::WizWar1.Properties.Resources.card_border1;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(253, 350);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// CardPreview
			// 
			this.BackColor = System.Drawing.Color.CornflowerBlue;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.pictureBox1);
			this.Name = "CardPreview";
			this.Size = new System.Drawing.Size(249, 348);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TextBox cardText;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label cardTitle;
	}
}