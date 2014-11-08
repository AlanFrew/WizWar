using System;
using System.Windows.Forms;

namespace WizWar1 {
    internal partial class InfoPane : Form {
        private UIControl myUI;

        public InfoPane(UIControl tMyUI) {
            InitializeComponent();
            myUI = tMyUI;
        }

        private void label2_Click(object sender, EventArgs e) {

        }

        private void InfoPane_Load(object sender, EventArgs e) {
        }

        internal void QueryWizard(Wizard w) {
            label2.Show();
            label4.Show();
            label5.Show();
            progressBar1.Show();
            WizardName.Text = w.Name;
            label3.Text = "Inventory";
            if (w.hit_points <= 15) {
                progressBar1.Maximum = 15;
            }
            else {
                progressBar1.Maximum = w.hit_points;
            }
                
            progressBar1.Minimum = 0;
            progressBar1.Value = w.hit_points;
            HitPointsNumber.Text = w.hit_points.ToString();

            InventoryListBox.Items.Clear();
            foreach (IItem i in w.Inventory) {
                InventoryListBox.Items.Add(i);
            }

            StatusEffectsListBox.Items.Clear();
            foreach (Effect e in GameState.DurationEffects) {
                if (e.target == w) {
                    StatusEffectsListBox.Items.Add(e.ToString());
                }
            }

            label5.Text = w.Hand.Count.ToString();
            myUI.State = UIState.Previous;
        }

        internal void QuerySquare(Square s) {
            WizardName.Text = "Square " + s.ID.ToString();
            progressBar1.Hide();
            label3.Text = "Items Here";
            InventoryListBox.Items.Clear();
            foreach (ICarriable i in s.CarriablesHere) {
                InventoryListBox.Items.Add(i);
            }

            label2.Hide();
            label4.Hide();
            label5.Hide();
            myUI.State = UIState.Previous;
        }

        private void QueryWizardButton_Click(object sender, EventArgs e) {
            myUI.State = UIState.Querying;
        }

        private void QuerySquareButton_Click(object sender, EventArgs e) {
            myUI.State = UIState.QueryingSquare;
        }

        private void InfoPane_FormClosing(Object sender, FormClosingEventArgs e) {
            myUI.myInfo = null;
        }
    }

}
