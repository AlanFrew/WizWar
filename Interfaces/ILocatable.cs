using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    interface ILocatable : ITarget {
        double X {
            get;
            set;
        }

        double Y {
            get;
            set;
        }
    }
}
