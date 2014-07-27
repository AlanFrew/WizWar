namespace WizWar1
{
    partial class Form1 {
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
            this.TestLoSButton = new System.Windows.Forms.Button();
            this.LoSCoordButton = new System.Windows.Forms.Button();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.StateLabel = new System.Windows.Forms.Label();
            this.ManualLoSTarget = new System.Windows.Forms.Button();
            this.BreakPointButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TestLoSButton
            // 
            this.TestLoSButton.Location = new System.Drawing.Point(456, 425);
            this.TestLoSButton.Name = "TestLoSButton";
            this.TestLoSButton.Size = new System.Drawing.Size(75, 23);
            this.TestLoSButton.TabIndex = 0;
            this.TestLoSButton.TabStop = false;
            this.TestLoSButton.Text = "Test LoS";
            this.TestLoSButton.UseVisualStyleBackColor = true;
            this.TestLoSButton.Click += new System.EventHandler(this.TestLoSButton_Click);
            // 
            // LoSCoordButton
            // 
            this.LoSCoordButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.LoSCoordButton.Location = new System.Drawing.Point(456, 396);
            this.LoSCoordButton.Name = "LoSCoordButton";
            this.LoSCoordButton.Size = new System.Drawing.Size(75, 23);
            this.LoSCoordButton.TabIndex = 1;
            this.LoSCoordButton.Text = "Test Coords";
            this.LoSCoordButton.UseVisualStyleBackColor = true;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(456, 370);
            this.maskedTextBox1.Mask = "099";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(25, 20);
            this.maskedTextBox1.TabIndex = 2;
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Location = new System.Drawing.Point(506, 370);
            this.maskedTextBox2.Mask = "099";
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(25, 20);
            this.maskedTextBox2.TabIndex = 3;
            // 
            // StateLabel
            // 
            this.StateLabel.AutoSize = true;
            this.StateLabel.Location = new System.Drawing.Point(456, 290);
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(35, 13);
            this.StateLabel.TabIndex = 5;
            this.StateLabel.Text = "label1";
            // 
            // ManualLoSTarget
            // 
            this.ManualLoSTarget.Enabled = false;
            this.ManualLoSTarget.Location = new System.Drawing.Point(446, 454);
            this.ManualLoSTarget.Name = "ManualLoSTarget";
            this.ManualLoSTarget.Size = new System.Drawing.Size(85, 23);
            this.ManualLoSTarget.TabIndex = 6;
            this.ManualLoSTarget.Text = "Manual Target";
            this.ManualLoSTarget.UseVisualStyleBackColor = true;
            this.ManualLoSTarget.Click += new System.EventHandler(this.ManualLoSTarget_Click);
            // 
            // BreakPointButton
            // 
            this.BreakPointButton.Location = new System.Drawing.Point(456, 13);
            this.BreakPointButton.Name = "BreakPointButton";
            this.BreakPointButton.Size = new System.Drawing.Size(75, 23);
            this.BreakPointButton.TabIndex = 7;
            this.BreakPointButton.Text = "Breakpoint";
            this.BreakPointButton.UseVisualStyleBackColor = true;
            this.BreakPointButton.Click += new System.EventHandler(this.BreakPointButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(543, 513);
            this.Controls.Add(this.BreakPointButton);
            this.Controls.Add(this.ManualLoSTarget);
            this.Controls.Add(this.StateLabel);
            this.Controls.Add(this.maskedTextBox2);
            this.Controls.Add(this.maskedTextBox1);
            this.Controls.Add(this.LoSCoordButton);
            this.Controls.Add(this.TestLoSButton);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoved);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button TestLoSButton;
        private System.Windows.Forms.Button LoSCoordButton;
        public System.Windows.Forms.MaskedTextBox maskedTextBox1;
        public System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.Label StateLabel;
        private System.Windows.Forms.Button ManualLoSTarget;
        private System.Windows.Forms.Button BreakPointButton;



    }
}

