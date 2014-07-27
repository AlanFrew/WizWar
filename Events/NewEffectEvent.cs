using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class NewEffectEvent : Event {
    internal Effect myEffect;
    public NewEffectEvent(Effect tEffect) {
        myEffect = tEffect;
    }
}
}
