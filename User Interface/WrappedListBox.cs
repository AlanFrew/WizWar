using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WizWar1 {
	class WrappedListBox<T> : ListBox where T : class {    //this class ended up doing nothing
		public void Add(Object item) {
			base.Items.Add(item);
		}

		public new T SelectedItem {
			get { return base.SelectedItem as T; }
		}
	}
}