namespace WizWar1 {
    partial class InfoPane {
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
            this.WizardName = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.InventoryListBox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.StatusEffectsListBox = new System.Windows.Forms.ListBox();
            this.StatusEffectsLabel = new System.Windows.Forms.Label();
            this.QueryWizardButton = new System.Windows.Forms.Button();
            this.QuerySquareButton = new System.Windows.Forms.Button();
            this.HitPointsNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // WizardName
            // 
            this.WizardName.AutoSize = true;
            this.WizardName.Location = new System.Drawing.Point(12, 9);
            this.WizardName.Name = "WizardName";
            this.WizardName.Size = new System.Drawing.Size(102, 13);
            this.WizardName.TabIndex = 0;
            this.WizardName.Text = "No Wizard Selected";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(225, 27);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(125, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hit Points:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(231, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Inventory";
            // 
            // InventoryListBox
            // 
            this.InventoryListBox.FormattingEnabled = true;
            this.InventoryListBox.Location = new System.Drawing.Point(225, 87);
            this.InventoryListBox.Name = "InventoryListBox";
            this.InventoryListBox.Size = new System.Drawing.Size(120, 95);
            this.InventoryListBox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Cards in Hand: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(87, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "--";
            // 
            // StatusEffectsListBox
            // 
            this.StatusEffectsListBox.FormattingEnabled = true;
            this.StatusEffectsListBox.Location = new System.Drawing.Point(22, 87);
            this.StatusEffectsListBox.Name = "StatusEffectsListBox";
            this.StatusEffectsListBox.Size = new System.Drawing.Size(180, 95);
            this.StatusEffectsListBox.TabIndex = 8;
            // 
            // StatusEffectsLabel
            // 
            this.StatusEffectsLabel.AutoSize = true;
            this.StatusEffectsLabel.Location = new System.Drawing.Point(19, 69);
            this.StatusEffectsLabel.Name = "StatusEffectsLabel";
            this.StatusEffectsLabel.Size = new System.Drawing.Size(73, 13);
            this.StatusEffectsLabel.TabIndex = 9;
            this.StatusEffectsLabel.Text = "Status Effects";
            // 
            // QueryWizardButton
            // 
            this.QueryWizardButton.Location = new System.Drawing.Point(22, 187);
            this.QueryWizardButton.Name = "QueryWizardButton";
            this.QueryWizardButton.Size = new System.Drawing.Size(92, 23);
            this.QueryWizardButton.TabIndex = 10;
            this.QueryWizardButton.Text = "Query Wizard";
            this.QueryWizardButton.UseVisualStyleBackColor = true;
            this.QueryWizardButton.Click += new System.EventHandler(this.QueryWizardButton_Click);
            // 
            // QuerySquareButton
            // 
            this.QuerySquareButton.Location = new System.Drawing.Point(234, 188);
            this.QuerySquareButton.Name = "QuerySquareButton";
            this.QuerySquareButton.Size = new System.Drawing.Size(87, 23);
            this.QuerySquareButton.TabIndex = 11;
            this.QuerySquareButton.Text = "Query Square";
            this.QuerySquareButton.UseVisualStyleBackColor = true;
            this.QuerySquareButton.Click += new System.EventHandler(this.QuerySquareButton_Click);
            // 
            // HitPointsNumber
            // 
            this.HitPointsNumber.AutoSize = true;
            this.HitPointsNumber.Location = new System.Drawing.Point(315, 11);
            this.HitPointsNumber.Name = "HitPointsNumber";
            this.HitPointsNumber.Size = new System.Drawing.Size(35, 13);
            this.HitPointsNumber.TabIndex = 12;
            this.HitPointsNumber.Text = "label1";
            // 
            // InfoPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 220);
            this.Controls.Add(this.HitPointsNumber);
            this.Controls.Add(this.QuerySquareButton);
            this.Controls.Add(this.QueryWizardButton);
            this.Controls.Add(this.StatusEffectsLabel);
            this.Controls.Add(this.StatusEffectsListBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.InventoryListBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.WizardName);
            this.Name = "InfoPane";
            this.Text = "InfoPane";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InfoPane_FormClosing);
            this.Load += new System.EventHandler(this.InfoPane_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WizardName;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox InventoryListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox StatusEffectsListBox;
        private System.Windows.Forms.Label StatusEffectsLabel;
        private System.Windows.Forms.Button QueryWizardButton;
        private System.Windows.Forms.Button QuerySquareButton;
        private System.Windows.Forms.Label HitPointsNumber;
    }
}