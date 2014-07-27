using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
interface ICard : ITarget {
    String Name {
        get;
        set;
    }



    String ToString();
}
}
