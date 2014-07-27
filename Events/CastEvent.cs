using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class CastEvent : Event {
    public ISpell SpellBeingCast;

    public CastEvent(ISpell tSpellBeingCast) {
        SpellBeingCast = tSpellBeingCast;
    }
}
}
