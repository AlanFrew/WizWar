using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class TurnStartEvent : Event {
    public Wizard nextWizard;
    public TurnStartEvent(Wizard tNextWizard) {
        nextWizard = tNextWizard;
    }
}
}
