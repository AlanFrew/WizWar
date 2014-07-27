using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace WizWar1 {
    public partial class DummyForm : Form {
        public DummyForm() {
            InitializeComponent();
            
        }

        private void DummyForm_Load(object sender, EventArgs e) {
            GameState.Initialize();

            UIControl player1UI = new UIControl(new Point(0, 0));
            UIControl player2UI = new UIControl(new Point(650, 0));

            //Form1 myOtherForm = new Form1();
            //myOtherForm.StartPosition = FormStartPosition.Manual;
            //myOtherForm.Location = new Point(640, 0);
            //myOtherForm.Show();
            //this.SendToBack();

            GameState.startNewGame(2);
            GameState.ActivePlayer = player1UI.myWizard;
            GameState.PriorityHolder = GameState.ActivePlayer.myUI;
            GameState.ActivePlayer.myUI.State = UIState.Normal;

            //var testList = new RobustList<int>();
            //testList.Add(3);
            //testList.Add(7);
            //testList.Add(2);
            //testList.Remove(2);
            //testList.Add(4);
            //testList.Remove(7);


            //bool boolean = true;
            //foreach (int i in testList) {
            //    testList.Remove(3);
            //    if (boolean == true) {
            //        testList.Add(8);
            //        boolean = false;
            //    }
            //    MessageBox.Show(i.ToString());
            //}
            
        }
    }
}
