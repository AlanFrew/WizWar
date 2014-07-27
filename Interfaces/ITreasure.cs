using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    interface ITreasure : IItem {
        Wizard Owner {
            get;
            set;
        }
    }
}
