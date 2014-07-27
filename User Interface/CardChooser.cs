using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWar1 {
    public partial class CardChooser : Form {
        internal WindowCallback CallBack;

        public CardChooser() {
            InitializeComponent();
        }

        internal CardChooser(WindowCallback tCallBack) {
            CallBack = tCallBack;
        }

        private void button1_Click(object sender, EventArgs e) {
            CallBack.SetFlowControl(Redirect.Proceed, 1.0);
            CallBack.AdditionalInfo = textBox1.Text;
            this.Close();
        }
    }
}
