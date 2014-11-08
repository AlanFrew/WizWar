using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWar1 {
	public partial class TypedListBox2<T> : ListBox where T : class {
		public TypedListBox2() {
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe) {
			base.OnPaint(pe);
		}

		public new T SelectedItem {
			get { return base.SelectedItem as T; }
		}

		public List<T> Items {
			get {
				var result = new T[base.Items.Count];

				base.Items.CopyTo(result, 0);

				return result.ToList();
			}
		}
		public void Add(Object item) {
			base.Items.Add(item);
		}
	}
}
