namespace WizWar1 {
    partial class SquareInfoPane {
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
            this.ItemsInSquareListBox = new System.Windows.Forms.ListBox();
            this.PickUpButton = new System.Windows.Forms.Button();
            this.WizardsHereListBox = new System.Windows.Forms.ListBox();
            this.CreationsHereListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ItemsInSquareListBox
            // 
            this.ItemsInSquareListBox.FormattingEnabled = true;
            this.ItemsInSquareListBox.Location = new System.Drawing.Point(5, 21);
            this.ItemsInSquareListBox.Name = "ItemsInSquareListBox";
            this.ItemsInSquareListBox.Size = new System.Drawing.Size(120, 69);
            this.ItemsInSquareListBox.TabIndex = 0;
            this.ItemsInSquareListBox.SelectedIndexChanged += new System.EventHandler(this.ItemsHereListBox_SelectedIndexChanged);
            // 
            // PickUpButton
            // 
            this.PickUpButton.Enabled = false;
            this.PickUpButton.Location = new System.Drawing.Point(21, 98);
            this.PickUpButton.Name = "PickUpButton";
            this.PickUpButton.Size = new System.Drawing.Size(84, 23);
            this.PickUpButton.TabIndex = 1;
            this.PickUpButton.Text = "Pick Up Item";
            this.PickUpButton.UseVisualStyleBackColor = true;
            this.PickUpButton.Click += new System.EventHandler(this.PickUpButton_Click);
            // 
            // WizardsHereListBox
            // 
            this.WizardsHereListBox.FormattingEnabled = true;
            this.WizardsHereListBox.Location = new System.Drawing.Point(132, 21);
            this.WizardsHereListBox.Name = "WizardsHereListBox";
            this.WizardsHereListBox.Size = new System.Drawing.Size(120, 69);
            this.WizardsHereListBox.TabIndex = 2;
            // 
            // CreationsHereListBox
            // 
            this.CreationsHereListBox.FormattingEnabled = true;
            this.CreationsHereListBox.Location = new System.Drawing.Point(132, 115);
            this.CreationsHereListBox.Name = "CreationsHereListBox";
            this.CreationsHereListBox.Size = new System.Drawing.Size(120, 69);
            this.CreationsHereListBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Items Here";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Wizards Here";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Creations Here";
            // 
            // SquareInfoPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 190);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CreationsHereListBox);
            this.Controls.Add(this.WizardsHereListBox);
            this.Controls.Add(this.PickUpButton);
            this.Controls.Add(this.ItemsInSquareListBox);
            this.Name = "SquareInfoPane";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.SquareInfoPane_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ItemsInSquareListBox;
        private System.Windows.Forms.Button PickUpButton;
        private System.Windows.Forms.ListBox WizardsHereListBox;
        private System.Windows.Forms.ListBox CreationsHereListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}