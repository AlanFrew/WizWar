using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWar1 {
public partial class TargetChooser : Form {
    private ISpell spellToCast;
    private Square targetSquare;
    private UIControl myUI;

    internal TargetChooser(UIControl tMyUI, Square tTargetSquare, ISpell tSpellToCast) {
        targetSquare = tTargetSquare;
        spellToCast = tSpellToCast;
        myUI = tMyUI;
        InitializeComponent();

        this.WizardTargetListBox.SelectedIndexChanged += new System.EventHandler(this.OnSelectedWizardTargetListBox);
        this.ItemsTargetListBox.SelectedIndexChanged += new System.EventHandler(this.OnSelectedItemTargetListBox);
        this.ObstructionTargetListBox.SelectedIndexChanged += new System.EventHandler(this.OnSelectedObstructionTargetListBox);

        WizardTargetButton.Enabled = false;
        ItemTargetButton.Enabled = false;
        ObstructionTargetButton.Enabled = false;
        SquareTargetButton.Enabled = false;
    }

    internal void ValidateTarget() {
        if (spellToCast.IsValidSpellTargetType(TargetTypes.Wall) || spellToCast.IsValidSpellTargetType(TargetTypes.Creation)) {
            foreach (Creation c in targetSquare.creationsHere) {
                ObstructionTargetListBox.Items.Add(c);
            }

            //deprecated now that wallspaces can be targeted directly
            //er...can they??
            //foreach (Direction d in Enum.GetValues(typeof(Direction))) {
            //    if (targetSquare.LookForWall(d) != null) {
            //        ObstructionTargetListBox.Items.Add(targetSquare.LookForWall(d));
            //    }
            //}

            //obstructions prevent targeting of other objects
            if (ObstructionTargetListBox.Items.Count > 0) {
                return;
            }
        }

        if (spellToCast.IsValidSpellTargetType(TargetTypes.Square)) {
            SquareTargetButton.Enabled = true;
        }

        if (spellToCast.IsValidSpellTargetType(TargetTypes.Wizard)) {
            foreach (Wizard w in GameState.wizards) {
                if (w.X == targetSquare.X && w.Y == targetSquare.Y) {
                    WizardTargetListBox.Items.Add(w);
                }
            }  
        }

        if (spellToCast.IsValidSpellTargetType(TargetTypes.Item)) {
            foreach (Item i in targetSquare.ItemsHere) {
                ItemsTargetListBox.Items.Add(i);
            }
        }
    }

    private void OnClickWizardTargetButton(object sender, EventArgs e) {
        if (WizardTargetListBox.SelectedItem != null) {
            if (spellToCast.IsValidSpellTargetParent(WizardTargetListBox.SelectedItem as Wizard, myUI.myWizard)) {
                TargetingEvent a = new TargetingEvent(WizardTargetListBox.SelectedItem as Wizard);
                if (GameState.InitialUltimatum(a) == Redirect.Proceed) {
                    myUI.myControl.TargetValidated(WizardTargetListBox.SelectedItem as Wizard, TargetTypes.Wizard);
                    this.Close();
                }
            }
        }
    }

    private void OnClickItemTargetButton(object sender, EventArgs e) {
        if (ItemsTargetListBox.SelectedItem != null) {
            if (spellToCast.IsValidSpellTargetParent(ItemsTargetListBox.SelectedItem as IItem, myUI.myWizard)) {
                myUI.myControl.TargetValidated(ItemsTargetListBox.SelectedItem as IItem, TargetTypes.Item);
                this.Close();
            }
        }
    }

    private void OnClickObstructionTargetButton(object sender, EventArgs e) {
        Object o = ObstructionTargetListBox.SelectedItem;
        if (o == null) {
            return;
        }

        //being a creation takes precedence over being a wall, I think
        if (o is ICreation) {
            if (spellToCast.IsValidSpellTargetParent(o as ICreation, myUI.myWizard)) {
                myUI.myControl.TargetValidated(o as ICreation, TargetTypes.Creation);
                this.Close();
                return;
            }
        }

        if (o is IWall) {
            if (spellToCast.IsValidSpellTargetParent(o as IWall, myUI.myWizard)) {
                myUI.myControl.TargetValidated(o as IWall, TargetTypes.Wall);
                this.Close();
                return;
            }
        }
    }

    private void OnClickSquareTargetButton(object sender, EventArgs e) {
        if (spellToCast.IsValidSpellTargetParent(targetSquare, myUI.myWizard)) {
            myUI.myControl.TargetValidated(targetSquare, TargetTypes.Square);
            this.Close();
        }
    }

    private void OnSelectedWizardTargetListBox(object sender, EventArgs e) {
        WizardTargetButton.Enabled = true;
    }

    private void OnSelectedItemTargetListBox(object sender, EventArgs e) {
        ItemTargetButton.Enabled = true;
    }

    private void OnSelectedObstructionTargetListBox(object sender, EventArgs e) {
        ObstructionTargetButton.Enabled = true;
    }

}
}
