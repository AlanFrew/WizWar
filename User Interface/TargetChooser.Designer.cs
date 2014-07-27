namespace WizWar1 {
    partial class TargetChooser {
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
            this.label1 = new System.Windows.Forms.Label();
            this.WizardTargetListBox = new System.Windows.Forms.ListBox();
            this.ItemsTargetListBox = new System.Windows.Forms.ListBox();
            this.ObstructionTargetListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SquareTargetButton = new System.Windows.Forms.Button();
            this.WizardTargetButton = new System.Windows.Forms.Button();
            this.ItemTargetButton = new System.Windows.Forms.Button();
            this.ObstructionTargetButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wizards";
            // 
            // WizardTargetListBox
            // 
            this.WizardTargetListBox.FormattingEnabled = true;
            this.WizardTargetListBox.Location = new System.Drawing.Point(16, 30);
            this.WizardTargetListBox.Name = "WizardTargetListBox";
            this.WizardTargetListBox.Size = new System.Drawing.Size(120, 95);
            this.WizardTargetListBox.TabIndex = 1;
            // 
            // ItemsTargetListBox
            // 
            this.ItemsTargetListBox.FormattingEnabled = true;
            this.ItemsTargetListBox.Location = new System.Drawing.Point(143, 30);
            this.ItemsTargetListBox.Name = "ItemsTargetListBox";
            this.ItemsTargetListBox.Size = new System.Drawing.Size(120, 95);
            this.ItemsTargetListBox.TabIndex = 2;
            // 
            // ObstructionTargetListBox
            // 
            this.ObstructionTargetListBox.FormattingEnabled = true;
            this.ObstructionTargetListBox.Location = new System.Drawing.Point(270, 30);
            this.ObstructionTargetListBox.Name = "ObstructionTargetListBox";
            this.ObstructionTargetListBox.Size = new System.Drawing.Size(120, 95);
            this.ObstructionTargetListBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Items";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(270, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Creations and Walls";
            // 
            // SquareTargetButton
            // 
            this.SquareTargetButton.Location = new System.Drawing.Point(362, 133);
            this.SquareTargetButton.Name = "SquareTargetButton";
            this.SquareTargetButton.Size = new System.Drawing.Size(75, 34);
            this.SquareTargetButton.TabIndex = 6;
            this.SquareTargetButton.Text = "Target Square";
            this.SquareTargetButton.UseVisualStyleBackColor = true;
            this.SquareTargetButton.Click += new System.EventHandler(this.OnClickSquareTargetButton);
            // 
            // WizardTargetButton
            // 
            this.WizardTargetButton.Location = new System.Drawing.Point(16, 131);
            this.WizardTargetButton.Name = "WizardTargetButton";
            this.WizardTargetButton.Size = new System.Drawing.Size(75, 23);
            this.WizardTargetButton.TabIndex = 7;
            this.WizardTargetButton.Text = "Confirm";
            this.WizardTargetButton.UseVisualStyleBackColor = true;
            this.WizardTargetButton.Click += new System.EventHandler(this.OnClickWizardTargetButton);
            // 
            // ItemTargetButton
            // 
            this.ItemTargetButton.Location = new System.Drawing.Point(143, 131);
            this.ItemTargetButton.Name = "ItemTargetButton";
            this.ItemTargetButton.Size = new System.Drawing.Size(75, 23);
            this.ItemTargetButton.TabIndex = 8;
            this.ItemTargetButton.Text = "Confirm";
            this.ItemTargetButton.UseVisualStyleBackColor = true;
            this.ItemTargetButton.Click += new System.EventHandler(this.OnClickItemTargetButton);
            // 
            // ObstructionTargetButton
            // 
            this.ObstructionTargetButton.Location = new System.Drawing.Point(270, 131);
            this.ObstructionTargetButton.Name = "ObstructionTargetButton";
            this.ObstructionTargetButton.Size = new System.Drawing.Size(75, 23);
            this.ObstructionTargetButton.TabIndex = 9;
            this.ObstructionTargetButton.Text = "Confirm";
            this.ObstructionTargetButton.UseVisualStyleBackColor = true;
            this.ObstructionTargetButton.Click += new System.EventHandler(this.OnClickObstructionTargetButton);
            // 
            // TargetChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 179);
            this.Controls.Add(this.ObstructionTargetButton);
            this.Controls.Add(this.ItemTargetButton);
            this.Controls.Add(this.WizardTargetButton);
            this.Controls.Add(this.SquareTargetButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ObstructionTargetListBox);
            this.Controls.Add(this.ItemsTargetListBox);
            this.Controls.Add(this.WizardTargetListBox);
            this.Controls.Add(this.label1);
            this.Name = "TargetChooser";
            this.Text = "TargetChooser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox WizardTargetListBox;
        private System.Windows.Forms.ListBox ItemsTargetListBox;
        private System.Windows.Forms.ListBox ObstructionTargetListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SquareTargetButton;
        private System.Windows.Forms.Button WizardTargetButton;
        private System.Windows.Forms.Button ItemTargetButton;
        private System.Windows.Forms.Button ObstructionTargetButton;
    }
}