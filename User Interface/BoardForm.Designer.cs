namespace WizWar1
{
    partial class BoardForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param Name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoardForm));
			this.TestLoSButton = new System.Windows.Forms.Button();
			this.LoSCoordButton = new System.Windows.Forms.Button();
			this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
			this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
			this.StateLabel = new System.Windows.Forms.Label();
			this.ManualLoSTarget = new System.Windows.Forms.Button();
			this.BreakPointButton = new System.Windows.Forms.Button();
			this.ControlPanel = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.CurrentHealthLabel = new System.Windows.Forms.Label();
			this.ContinueTimer = new System.Windows.Forms.ProgressBar();
			this.NumberButton = new System.Windows.Forms.Button();
			this.InstructionText = new System.Windows.Forms.Label();
			this.CastButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.Inventory = new System.Windows.Forms.ListBox();
			this.StackOSpells = new System.Windows.Forms.TreeView();
			this.DiscardButton = new System.Windows.Forms.Button();
			this.DrawButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.HandCountLabel = new System.Windows.Forms.Label();
			this.CounteractButton = new System.Windows.Forms.Button();
			this.DropItemButton = new System.Windows.Forms.Button();
			this.UseItemButton = new System.Windows.Forms.Button();
			this.ShowInfoButton = new System.Windows.Forms.Button();
			this.EndTurnButton = new System.Windows.Forms.Button();
			this.ContinueButton = new System.Windows.Forms.Button();
			this.HandOCards = new System.Windows.Forms.ListBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.cardPreview1 = new WizWar1.CardPreview();
			this.ControlPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// TestLoSButton
			// 
			resources.ApplyResources(this.TestLoSButton, "TestLoSButton");
			this.TestLoSButton.Name = "TestLoSButton";
			this.TestLoSButton.TabStop = false;
			this.TestLoSButton.UseVisualStyleBackColor = true;
			this.TestLoSButton.Click += new System.EventHandler(this.TestLoSButton_Click);
			// 
			// LoSCoordButton
			// 
			this.LoSCoordButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			resources.ApplyResources(this.LoSCoordButton, "LoSCoordButton");
			this.LoSCoordButton.Name = "LoSCoordButton";
			this.LoSCoordButton.UseVisualStyleBackColor = true;
			// 
			// maskedTextBox1
			// 
			resources.ApplyResources(this.maskedTextBox1, "maskedTextBox1");
			this.maskedTextBox1.Name = "maskedTextBox1";
			// 
			// maskedTextBox2
			// 
			resources.ApplyResources(this.maskedTextBox2, "maskedTextBox2");
			this.maskedTextBox2.Name = "maskedTextBox2";
			// 
			// StateLabel
			// 
			resources.ApplyResources(this.StateLabel, "StateLabel");
			this.StateLabel.Name = "StateLabel";
			// 
			// ManualLoSTarget
			// 
			resources.ApplyResources(this.ManualLoSTarget, "ManualLoSTarget");
			this.ManualLoSTarget.Name = "ManualLoSTarget";
			this.ManualLoSTarget.UseVisualStyleBackColor = true;
			this.ManualLoSTarget.Click += new System.EventHandler(this.ManualLoSTarget_Click);
			// 
			// BreakPointButton
			// 
			this.BreakPointButton.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(this.BreakPointButton, "BreakPointButton");
			this.BreakPointButton.Name = "BreakPointButton";
			this.BreakPointButton.UseVisualStyleBackColor = false;
			this.BreakPointButton.Click += new System.EventHandler(this.BreakPointButton_Click);
			// 
			// ControlPanel
			// 
			this.ControlPanel.BackColor = System.Drawing.Color.Gainsboro;
			this.ControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ControlPanel.Controls.Add(this.label1);
			this.ControlPanel.Controls.Add(this.CurrentHealthLabel);
			this.ControlPanel.Controls.Add(this.ContinueTimer);
			this.ControlPanel.Controls.Add(this.NumberButton);
			this.ControlPanel.Controls.Add(this.StateLabel);
			this.ControlPanel.Controls.Add(this.InstructionText);
			this.ControlPanel.Controls.Add(this.CastButton);
			this.ControlPanel.Controls.Add(this.CancelButton);
			this.ControlPanel.Controls.Add(this.Inventory);
			this.ControlPanel.Controls.Add(this.StackOSpells);
			this.ControlPanel.Controls.Add(this.DiscardButton);
			this.ControlPanel.Controls.Add(this.DrawButton);
			this.ControlPanel.Controls.Add(this.label3);
			this.ControlPanel.Controls.Add(this.label2);
			this.ControlPanel.Controls.Add(this.HandCountLabel);
			this.ControlPanel.Controls.Add(this.CounteractButton);
			this.ControlPanel.Controls.Add(this.DropItemButton);
			this.ControlPanel.Controls.Add(this.UseItemButton);
			this.ControlPanel.Controls.Add(this.ShowInfoButton);
			this.ControlPanel.Controls.Add(this.EndTurnButton);
			this.ControlPanel.Controls.Add(this.ContinueButton);
			this.ControlPanel.Controls.Add(this.HandOCards);
			resources.ApplyResources(this.ControlPanel, "ControlPanel");
			this.ControlPanel.Name = "ControlPanel";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// CurrentHealthLabel
			// 
			resources.ApplyResources(this.CurrentHealthLabel, "CurrentHealthLabel");
			this.CurrentHealthLabel.Name = "CurrentHealthLabel";
			// 
			// ContinueTimer
			// 
			this.ContinueTimer.ForeColor = System.Drawing.Color.MediumTurquoise;
			resources.ApplyResources(this.ContinueTimer, "ContinueTimer");
			this.ContinueTimer.Name = "ContinueTimer";
			// 
			// NumberButton
			// 
			resources.ApplyResources(this.NumberButton, "NumberButton");
			this.NumberButton.Name = "NumberButton";
			this.NumberButton.UseVisualStyleBackColor = true;
			this.NumberButton.Click += new System.EventHandler(this.NumberButton_Click);
			// 
			// InstructionText
			// 
			resources.ApplyResources(this.InstructionText, "InstructionText");
			this.InstructionText.ForeColor = System.Drawing.Color.Red;
			this.InstructionText.Name = "InstructionText";
			// 
			// CastButton
			// 
			resources.ApplyResources(this.CastButton, "CastButton");
			this.CastButton.Name = "CastButton";
			this.CastButton.UseVisualStyleBackColor = true;
			this.CastButton.Click += new System.EventHandler(this.CastButton_Click);
			// 
			// CancelButton
			// 
			resources.ApplyResources(this.CancelButton, "CancelButton");
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.UseVisualStyleBackColor = true;
			this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// Inventory
			// 
			this.Inventory.FormattingEnabled = true;
			resources.ApplyResources(this.Inventory, "Inventory");
			this.Inventory.Name = "Inventory";
			// 
			// StackOSpells
			// 
			resources.ApplyResources(this.StackOSpells, "StackOSpells");
			this.StackOSpells.Name = "StackOSpells";
			// 
			// DiscardButton
			// 
			resources.ApplyResources(this.DiscardButton, "DiscardButton");
			this.DiscardButton.Name = "DiscardButton";
			this.DiscardButton.UseVisualStyleBackColor = true;
			this.DiscardButton.Click += new System.EventHandler(this.DiscardButton_Click);
			// 
			// DrawButton
			// 
			resources.ApplyResources(this.DrawButton, "DrawButton");
			this.DrawButton.Name = "DrawButton";
			this.DrawButton.UseVisualStyleBackColor = true;
			this.DrawButton.Click += new System.EventHandler(this.DrawButton_Click);
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// HandCountLabel
			// 
			resources.ApplyResources(this.HandCountLabel, "HandCountLabel");
			this.HandCountLabel.Name = "HandCountLabel";
			// 
			// CounteractButton
			// 
			resources.ApplyResources(this.CounteractButton, "CounteractButton");
			this.CounteractButton.Name = "CounteractButton";
			this.CounteractButton.UseVisualStyleBackColor = true;
			this.CounteractButton.Click += new System.EventHandler(this.CounteractButton_Click);
			// 
			// DropItemButton
			// 
			resources.ApplyResources(this.DropItemButton, "DropItemButton");
			this.DropItemButton.Name = "DropItemButton";
			this.DropItemButton.UseVisualStyleBackColor = true;
			this.DropItemButton.Click += new System.EventHandler(this.DropItemButton_Click);
			// 
			// UseItemButton
			// 
			resources.ApplyResources(this.UseItemButton, "UseItemButton");
			this.UseItemButton.Name = "UseItemButton";
			this.UseItemButton.UseVisualStyleBackColor = true;
			this.UseItemButton.Click += new System.EventHandler(this.UseItemButton_Click);
			// 
			// ShowInfoButton
			// 
			resources.ApplyResources(this.ShowInfoButton, "ShowInfoButton");
			this.ShowInfoButton.Name = "ShowInfoButton";
			this.ShowInfoButton.UseVisualStyleBackColor = true;
			this.ShowInfoButton.Click += new System.EventHandler(this.ShowInfoButton_Click);
			// 
			// EndTurnButton
			// 
			resources.ApplyResources(this.EndTurnButton, "EndTurnButton");
			this.EndTurnButton.Name = "EndTurnButton";
			this.EndTurnButton.UseVisualStyleBackColor = true;
			this.EndTurnButton.Click += new System.EventHandler(this.EndTurnButton_Click);
			// 
			// ContinueButton
			// 
			resources.ApplyResources(this.ContinueButton, "ContinueButton");
			this.ContinueButton.Name = "ContinueButton";
			this.ContinueButton.UseVisualStyleBackColor = true;
			this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
			// 
			// HandOCards
			// 
			this.HandOCards.FormattingEnabled = true;
			resources.ApplyResources(this.HandOCards, "HandOCards");
			this.HandOCards.Name = "HandOCards";
			this.HandOCards.SelectedIndexChanged += new System.EventHandler(this.HandOCards_SelectedIndexChanged);
			this.HandOCards.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
			// 
			// cardPreview1
			// 
			this.cardPreview1.BackColor = System.Drawing.Color.CornflowerBlue;
			resources.ApplyResources(this.cardPreview1, "cardPreview1");
			this.cardPreview1.Name = "cardPreview1";
			// 
			// BoardForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DarkGray;
			this.ControlBox = false;
			this.Controls.Add(this.cardPreview1);
			this.Controls.Add(this.ControlPanel);
			this.Controls.Add(this.BreakPointButton);
			this.Controls.Add(this.ManualLoSTarget);
			this.Controls.Add(this.maskedTextBox2);
			this.Controls.Add(this.maskedTextBox1);
			this.Controls.Add(this.LoSCoordButton);
			this.Controls.Add(this.TestLoSButton);
			this.DoubleBuffered = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BoardForm";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoved);
			this.ControlPanel.ResumeLayout(false);
			this.ControlPanel.PerformLayout();
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
		  private System.Windows.Forms.Panel ControlPanel;
		  private System.Windows.Forms.Button ContinueButton;
		  private System.Windows.Forms.ListBox HandOCards;
		  private System.Windows.Forms.Button DiscardButton;
		  private System.Windows.Forms.Button DrawButton;
		  private System.Windows.Forms.Label label3;
		  private System.Windows.Forms.Label label2;
		  private System.Windows.Forms.Label HandCountLabel;
		  private System.Windows.Forms.Button CounteractButton;
		  private System.Windows.Forms.Button DropItemButton;
		  private System.Windows.Forms.Button UseItemButton;
		  private System.Windows.Forms.Button ShowInfoButton;
		  private System.Windows.Forms.Button EndTurnButton;
		  private System.Windows.Forms.ListBox Inventory;
		  private System.Windows.Forms.TreeView StackOSpells;
		  private System.Windows.Forms.Button CancelButton;
		  private System.Windows.Forms.Button CastButton;
		  private System.Windows.Forms.Label InstructionText;
		  private System.Windows.Forms.Button NumberButton;
		  private System.Windows.Forms.ToolTip toolTip1;
		  private CardPreview cardPreview1;
		  private System.Windows.Forms.ProgressBar ContinueTimer;
		  private System.Windows.Forms.Label CurrentHealthLabel;
		  private System.Windows.Forms.Label label1;



    }
}

