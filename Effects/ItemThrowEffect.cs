using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class ItemThrowEffect : Effect {
    Square start;
    Square finish;

    public ItemThrowEffect(Square tStart, Square tFinish) {
        start = tStart;
        finish = tFinish;
    }
}
}
