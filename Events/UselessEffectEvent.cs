using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class UselessEffectEvent : Event {
    public Effect me;

    public UselessEffectEvent(Effect tMe) {
        me = tMe;
    }
}
}
