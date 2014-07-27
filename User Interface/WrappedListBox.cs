using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWar1 {
class WrappedListBox : ListBox {    //this class ended up doing nothing
    private ControlPanel parent;

    public WrappedListBox() {
        
    }

    public WrappedListBox(Form tForm) {
    }

    public WrappedListBox(ControlPanel tParent) {
        parent = tParent;
    }

    public WrappedListBox(SelectionMode sm) {
        
    }

    public void Add(Object item) {
        Items.Add(item);
    }
}
}
