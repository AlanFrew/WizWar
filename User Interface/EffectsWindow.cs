using System;
using System.Windows.Forms;

namespace WizWar1 {
    public partial class EffectsWindow : Form {
        private UIControl myUI;
        internal EffectsWindow(UIControl tMyUI) {
            myUI = tMyUI;

            InitializeComponent();
            //foreach (Effect e in GameState.instantEffectStack) {
            //    if (e.target == myUI.myWizard) {
            //        PersonalEffectsListBox.Items.Add(e);
            //    }

            //    if (e as IItem != null) {
            //        if ((e as IItem).Carrier == myUI.myWizard) {
            //            PersonalEffectsListBox.Items.Add(e);
            //        }
            //    }

            //}

            foreach (Effect e in GameState.DurationEffects) {
                if (e.target == myUI.myWizard) {
                    PersonalEffectsListBox.Items.Add(e);
                }

                if (e as IItem != null) {
                    if ((e as IItem).Carrier == myUI.myWizard) {
                        PersonalEffectsListBox.Items.Add(e);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            if (PersonalEffectsListBox.SelectedItem != null) {
                if (myUI.CurrentAimable.IsValidTargetParent(PersonalEffectsListBox.SelectedItem as ITarget)) {
                    myUI.CurrentAimable.Target = PersonalEffectsListBox.SelectedItem as ITarget;
                }
            }
        }
    }
}
