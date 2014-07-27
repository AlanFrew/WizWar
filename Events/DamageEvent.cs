using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class DamageEvent : Event {
    internal DamageEffect myEffect;
    public DamageEvent(DamageEffect tEffect) {
        myEffect = tEffect;
    }
}
}
