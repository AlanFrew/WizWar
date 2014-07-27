﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWar1 {
public partial class SquareInfoPane : Form {
    private UIControl myUI;
    private Square querySquare;
    internal Square QuerySquare {
        get {
            return querySquare;
        }
        set {
            if (value != null) {
                querySquare = value;
            }
        }
    }
    internal SquareInfoPane(UIControl tMyUI, Square tQuerySquare) {
        InitializeComponent();

        myUI = tMyUI;
        myUI.mySquareInfo = this;
        querySquare = tQuerySquare;
    }

    private void SquareInfoPane_Load(object sender, EventArgs e) {
        RefreshAll();
    }

    private void PickUpButton_Click(object sender, EventArgs e) { // hopefully disabled if there is no item selected
        myUI.myWizard.pickUpItem(ItemsInSquareListBox.SelectedItem as IItem);
        ItemsHereRefresh();
    }

    private void ItemsHereListBox_SelectedIndexChanged(object sender, EventArgs e) {
        if (GameState.ActivePlayer == myUI.myWizard && myUI.myWizard.ContainingSquare == querySquare &&
        ItemsInSquareListBox.SelectedIndex != -1) {
            PickUpButton.Enabled = true;
        }
        else {
            PickUpButton.Enabled = false;
        }
    }

    public void ItemsHereRefresh() {
        ItemsInSquareListBox.Items.Clear();
        foreach (IItem item in querySquare.ItemsHere) {
            ItemsInSquareListBox.Items.Add(item);
        }
        ItemsHereListBox_SelectedIndexChanged(this, new EventArgs());
    }

    public void CreationsHereRefresh() {
        CreationsHereListBox.Items.Clear();
        foreach (ICreation creation in querySquare.creationsHere) {
            ItemsInSquareListBox.Items.Add(creation);
        }
    }

    public void WizardsRefresh() {
        WizardsHereListBox.Items.Clear();
        foreach (Wizard wizard in GameState.wizards) {
            if (wizard.ContainingSquare == querySquare) {
                WizardsHereListBox.Items.Add(wizard);
            }
        }
    }

    public void RefreshAll() {
        ItemsHereRefresh();
        CreationsHereRefresh();
        WizardsRefresh();
    }
}
}
