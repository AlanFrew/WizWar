using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class ExtraActionEffect : Effect {
    public override void OnRunChild() {
        (target as Wizard).myUI.myControl.numberCardsLeft++;
    }
}
}
