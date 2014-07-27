using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class DrawEvent : Event {
    public ICard CardDrawn;

    public DrawEvent(ICard tCard) {
        CardDrawn = tCard;
    }
}
}
