using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class MagicStone : Item {
    public MagicStone() {
        Name = "Magic Stone";
    }

    public override void OnActivationChild() {
        Activate();
    }

    void Activate() {
        throw new NotImplementedException();
    }

    //public override void OnResolution() {
    //    throw new NotImplementedException();
    //}
}
}
