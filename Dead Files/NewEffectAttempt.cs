using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class NewEffectAttempt : Event {
    Effect effect;

    public NewEffectAttempt() {
        //empty
    }

    public NewEffectAttempt(Effect tEffect) {
        effect = tEffect;
    }
}
}
