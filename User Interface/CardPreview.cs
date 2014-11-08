using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWar1 {
	public partial class CardPreview : UserControl {
		Image card_border; //Image.FromFile(@"Arena\card_border.png");
		Bitmap cardBorder;

		public CardPreview() {
			InitializeComponent();

			//cardBorder = new Bitmap(card_border);
			//cardBorder.MakeTransparent();
		}

		public TextBox CardText {
			get {
				return cardText;
			}
			set {
				cardText = value;
			}
		}

		public Label CardTitle {
			get {
				return cardTitle;
			}
			set {
				cardTitle = value;
			}
		}

		private void cardPreview_TextChanged(object sender, EventArgs e) {
			Size size = TextRenderer.MeasureText(cardText.Text, cardText.Font, new Size(cardText.Width, Int32.MaxValue), TextFormatFlags.WordBreak);

			cardText.Height = size.Height;

			cardText.Location = new Point(cardText.Location.X, (panel1.Height - cardText.Height) / 2);
		}
	}
}
