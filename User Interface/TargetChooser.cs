using System;
using System.Windows.Forms;

namespace WizWar1 {
public partial class TargetChooser : Form {
    private IAimable currentAimable;
    private Square targetSquare;
    private UIControl myUI;

    internal TargetChooser(UIControl tMyUI, Square tTargetSquare, IAimable tAimable) {
        targetSquare = tTargetSquare;
        currentAimable = tAimable;
        myUI = tMyUI;
        InitializeComponent();

        WizardTargetListBox.SelectedIndexChanged += new EventHandler(OnSelectedWizardTargetListBox);
        ItemsTargetListBox.SelectedIndexChanged += new EventHandler(OnSelectedItemTargetListBox);
        ObstructionTargetListBox.SelectedIndexChanged += new EventHandler(OnSelectedObstructionTargetListBox);

        WizardTargetButton.Enabled = false;
        ItemTargetButton.Enabled = false;
        ObstructionTargetButton.Enabled = false;
        SquareTargetButton.Enabled = false;
    }

    internal void ValidateTarget() {
        if (currentAimable.IsValidTargetType(TargetTypes.Wall) || currentAimable.IsValidTargetType(TargetTypes.Creation)) {
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

        if (currentAimable.IsValidTargetType(TargetTypes.Square)) {
            SquareTargetButton.Enabled = true;
        }

        if (currentAimable.IsValidTargetType(TargetTypes.Wizard)) {
            foreach (Wizard w in GameState.Wizards) {
                if (w.X == targetSquare.X && w.Y == targetSquare.Y) {
                    WizardTargetListBox.Items.Add(w);
                }
            }  
        }

        if (currentAimable.IsValidTargetType(TargetTypes.Item)) {
            foreach (Item i in targetSquare.ItemsHere) {
                ItemsTargetListBox.Items.Add(i);
            }
        }
    }

    private void OnClickWizardTargetButton(object sender, EventArgs e) {
        if (WizardTargetListBox.SelectedItem != null) {
            if (currentAimable.IsValidTargetParent(WizardTargetListBox.SelectedItem as Wizard)) {
                TargetingEvent a = new TargetingEvent(WizardTargetListBox.SelectedItem as Wizard, myUI.myWizard);
                if (GameState.InitialUltimatum(a) == Redirect.Proceed) {
                    myUI.myBoard.TargetValidated(WizardTargetListBox.SelectedItem as Wizard, TargetTypes.Wizard);
                    Close();
                }
            }
        }
    }

    private void OnClickItemTargetButton(object sender, EventArgs e) {
        if (ItemsTargetListBox.SelectedItem != null) {
            if (currentAimable.IsValidTargetParent(ItemsTargetListBox.SelectedItem as IItem)) {
                myUI.myBoard.TargetValidated(ItemsTargetListBox.SelectedItem as IItem, TargetTypes.Item);
                Close();
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
            if (currentAimable.IsValidTargetParent(o as ICreation)) {
                myUI.myBoard.TargetValidated(o as ICreation, TargetTypes.Creation);
                Close();
                return;
            }
        }

        if (o is IWall) {
            if (currentAimable.IsValidTargetParent(o as IWall)) {
                myUI.myBoard.TargetValidated(o as IWall, TargetTypes.Wall);
                Close();
                return;
            }
        }
    }

    private void OnClickSquareTargetButton(object sender, EventArgs e) {
        if (currentAimable.IsValidTargetParent(targetSquare)) {
            myUI.myBoard.TargetValidated(targetSquare, TargetTypes.Square);
            Close();
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
