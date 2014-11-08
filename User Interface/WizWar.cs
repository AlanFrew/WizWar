using System;
using System.Drawing;
using System.Windows.Forms;
using WizWar1.Properties;

namespace WizWar1 {
	public partial class WizWar : Form {
		public WizWar() {
			InitializeComponent();
		}

		private void WizWarForm_Load(object sender, EventArgs e) {
			GameState.Initialize(Settings.Default.GameMode == "Sandbox" ? 2 : 1);

			new UIControl(new Point(5, 5));
			new UIControl(new Point(655, 5));

			if (Settings.Default.GameMode == "Sandbox") {
				GameState.StartNewGame(2, 0);
			}
			else {
				GameState.StartNewGame(1, 1);
			}

			GameState.PriorityHolder = GameState.ActivePlayer.myUI;
			GameState.ActivePlayer.myUI.State = UIState.Normal;

			WindowState = FormWindowState.Minimized;
		}
	}
}