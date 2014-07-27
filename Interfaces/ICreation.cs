using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
interface ICreation : ILocatable {
    Wizard Creator {
        get;
    }

    void BecomeCreation();

}
}
