using WizWar1;

namespace WizWar1 {
    partial class ControlPanel2 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param Name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
			this.components = new System.ComponentModel.Container();
			this.CastButton = new System.Windows.Forms.Button();
			this.ListOItems = new System.Windows.Forms.ListBox();
			this.UseItemButton = new System.Windows.Forms.Button();
			this.ContinueButton = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.CounteractionButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.NumberButton = new System.Windows.Forms.Button();
			this.DropItemButton = new System.Windows.Forms.Button();
			this.CancelCastButton = new System.Windows.Forms.Button();
			this.EndTurnButton = new System.Windows.Forms.Button();
			this.ShowInfoButton = new System.Windows.Forms.Button();
			this.DrawButton = new System.Windows.Forms.Button();
			this.DiscardButton = new System.Windows.Forms.Button();
			this.SpellTree = new System.Windows.Forms.TreeView();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.InstructionText = new System.Windows.Forms.Label();
			this.HandOCards = new WrappedListBox<ICard>();
			this.SuspendLayout();
			// 
			// CastButton
			// 
			this.CastButton.Enabled = false;
			this.CastButton.Location = new System.Drawing.Point(66, 130);
			this.CastButton.Name = "CastButton";
			this.CastButton.Size = new System.Drawing.Size(63, 23);
			this.CastButton.TabIndex = 0;
			this.CastButton.Text = "Cast Spell";
			this.CastButton.UseVisualStyleBackColor = true;
			// 
			// ListOItems
			// 
			this.ListOItems.FormattingEnabled = true;
			this.ListOItems.Location = new System.Drawing.Point(387, 29);
			this.ListOItems.Name = "ListOItems";
			this.ListOItems.Size = new System.Drawing.Size(120, 95);
			this.ListOItems.TabIndex = 3;
			this.ListOItems.SelectedIndexChanged += new System.EventHandler(this.ListOItems_SelectedIndexChanged);
			// 
			// UseItemButton
			// 
			this.UseItemButton.Enabled = false;
			this.UseItemButton.Location = new System.Drawing.Point(387, 130);
			this.UseItemButton.Name = "UseItemButton";
			this.UseItemButton.Size = new System.Drawing.Size(57, 23);
			this.UseItemButton.TabIndex = 4;
			this.UseItemButton.Text = "Use Item";
			this.UseItemButton.UseVisualStyleBackColor = true;
			this.UseItemButton.Click += new System.EventHandler(this.UseItemButton_Click);
			// 
			// ContinueButton
			// 
			this.ContinueButton.Enabled = false;
			this.ContinueButton.Location = new System.Drawing.Point(512, 46);
			this.ContinueButton.Name = "ContinueButton";
			this.ContinueButton.Size = new System.Drawing.Size(75, 23);
			this.ContinueButton.TabIndex = 5;
			this.ContinueButton.Text = "Continue";
			this.ContinueButton.UseVisualStyleBackColor = true;
			this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(144, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(99, 13);
			this.label6.TabIndex = 11;
			this.label6.Text = "Spells on the Stack";
			// 
			// CounteractionButton
			// 
			this.CounteractionButton.Enabled = false;
			this.CounteractionButton.Location = new System.Drawing.Point(224, 130);
			this.CounteractionButton.Name = "CounteractionButton";
			this.CounteractionButton.Size = new System.Drawing.Size(75, 23);
			this.CounteractionButton.TabIndex = 12;
			this.CounteractionButton.Text = "Counteract";
			this.CounteractionButton.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(387, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(115, 13);
			this.label1.TabIndex = 14;
			this.label1.Text = "Items in Your Inventory";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99, 13);
			this.label2.TabIndex = 15;
			this.label2.Text = "Cards in Your Hand";
			// 
			// NumberButton
			// 
			this.NumberButton.Enabled = false;
			this.NumberButton.Location = new System.Drawing.Point(9, 130);
			this.NumberButton.Name = "NumberButton";
			this.NumberButton.Size = new System.Drawing.Size(59, 23);
			this.NumberButton.TabIndex = 16;
			this.NumberButton.Text = "Number";
			this.NumberButton.UseVisualStyleBackColor = true;
			this.NumberButton.Click += new System.EventHandler(this.NumberButton_Click);
			// 
			// DropItemButton
			// 
			this.DropItemButton.Enabled = false;
			this.DropItemButton.Location = new System.Drawing.Point(445, 130);
			this.DropItemButton.Name = "DropItemButton";
			this.DropItemButton.Size = new System.Drawing.Size(62, 23);
			this.DropItemButton.TabIndex = 17;
			this.DropItemButton.Text = "Drop Item";
			this.DropItemButton.UseVisualStyleBackColor = true;
			this.DropItemButton.Click += new System.EventHandler(this.DropItemButton_Click);
			// 
			// CancelCastButton
			// 
			this.CancelCastButton.Enabled = false;
			this.CancelCastButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.CancelCastButton.Location = new System.Drawing.Point(512, 14);
			this.CancelCastButton.Name = "CancelCastButton";
			this.CancelCastButton.Size = new System.Drawing.Size(75, 23);
			this.CancelCastButton.TabIndex = 19;
			this.CancelCastButton.Text = "Cancel";
			this.CancelCastButton.UseVisualStyleBackColor = true;
			this.CancelCastButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// EndTurnButton
			// 
			this.EndTurnButton.Location = new System.Drawing.Point(512, 131);
			this.EndTurnButton.Name = "EndTurnButton";
			this.EndTurnButton.Size = new System.Drawing.Size(75, 23);
			this.EndTurnButton.TabIndex = 20;
			this.EndTurnButton.Text = "End Turn";
			this.EndTurnButton.UseVisualStyleBackColor = true;
			this.EndTurnButton.Click += new System.EventHandler(this.EndTurnButton_Click);
			// 
			// ShowInfoButton
			// 
			this.ShowInfoButton.Location = new System.Drawing.Point(513, 101);
			this.ShowInfoButton.Name = "ShowInfoButton";
			this.ShowInfoButton.Size = new System.Drawing.Size(74, 23);
			this.ShowInfoButton.TabIndex = 21;
			this.ShowInfoButton.Text = "Show Info";
			this.ShowInfoButton.UseVisualStyleBackColor = true;
			this.ShowInfoButton.Click += new System.EventHandler(this.ShowInfoButton_Click);
			// 
			// DrawButton
			// 
			this.DrawButton.Location = new System.Drawing.Point(9, 158);
			this.DrawButton.Name = "DrawButton";
			this.DrawButton.Size = new System.Drawing.Size(51, 23);
			this.DrawButton.TabIndex = 22;
			this.DrawButton.Text = "Draw";
			this.DrawButton.UseVisualStyleBackColor = true;
			this.DrawButton.Click += new System.EventHandler(this.DrawButton_Click);
			// 
			// DiscardButton
			// 
			this.DiscardButton.Location = new System.Drawing.Point(66, 158);
			this.DiscardButton.Name = "DiscardButton";
			this.DiscardButton.Size = new System.Drawing.Size(63, 23);
			this.DiscardButton.TabIndex = 23;
			this.DiscardButton.Text = "Discard";
			this.DiscardButton.UseVisualStyleBackColor = true;
			this.DiscardButton.Click += new System.EventHandler(this.DiscardButton_Click);
			// 
			// SpellTree
			// 
			this.SpellTree.Location = new System.Drawing.Point(136, 29);
			this.SpellTree.Name = "SpellTree";
			this.SpellTree.Size = new System.Drawing.Size(245, 95);
			this.SpellTree.TabIndex = 24;
			this.SpellTree.Click += new System.EventHandler(this.SpellTree_Clicked);
			// 
			// InstructionText
			// 
			this.InstructionText.AutoSize = true;
			this.InstructionText.ForeColor = System.Drawing.Color.Red;
			this.InstructionText.Location = new System.Drawing.Point(163, 158);
			this.InstructionText.Name = "InstructionText";
			this.InstructionText.Size = new System.Drawing.Size(85, 13);
			this.InstructionText.TabIndex = 25;
			this.InstructionText.Text = "Instructions Text";
			// 
			// HandOCards
			// 
			this.HandOCards.FormattingEnabled = true;
			this.HandOCards.Location = new System.Drawing.Point(9, 29);
			this.HandOCards.Name = "HandOCards";
			this.HandOCards.Size = new System.Drawing.Size(120, 95);
			this.HandOCards.TabIndex = 26;
			this.HandOCards.SelectedIndexChanged += HandOCards_SelectedIndexChanged;
			this.HandOCards.MouseMove += OnMouseMove;
			this.MouseMove += OnMouseMove;
			// 
			// ControlPanel
			// 
			//this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			//this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(597, 187);
			this.Controls.Add(this.InstructionText);
			this.Controls.Add(this.SpellTree);
			this.Controls.Add(this.DiscardButton);
			this.Controls.Add(this.DrawButton);
			this.Controls.Add(this.ShowInfoButton);
			this.Controls.Add(this.EndTurnButton);
			this.Controls.Add(this.CancelCastButton);
			this.Controls.Add(this.DropItemButton);
			this.Controls.Add(this.NumberButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.CounteractionButton);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.ContinueButton);
			this.Controls.Add(this.UseItemButton);
			this.Controls.Add(this.ListOItems);
			this.Controls.Add(this.CastButton);
			this.Controls.Add(this.HandOCards);
			//this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ControlPanel";
			//this.ShowInTaskbar = false;
			this.Text = "CandyFloss";
			//this.Load += new System.EventHandler(this.ControlPanel_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CastButton;
        private System.Windows.Forms.ListBox ListOItems;
        private System.Windows.Forms.Button UseItemButton;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button CounteractionButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button NumberButton;
        private System.Windows.Forms.Button DropItemButton;
        private System.Windows.Forms.Button CancelCastButton;
        private System.Windows.Forms.Button EndTurnButton;
        private System.Windows.Forms.Button ShowInfoButton;
        private System.Windows.Forms.Button DrawButton;
        private System.Windows.Forms.Button DiscardButton;
        private System.Windows.Forms.TreeView SpellTree;
		  private System.Windows.Forms.ToolTip toolTip1;
		  private System.Windows.Forms.Label InstructionText;
		  private WrappedListBox<ICard> HandOCards;
    }
}