using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class Obstruction : Creation {
    public virtual bool IsPassable(Wizard tWizard) {
        return true;
    }

    public void OnEnterParent(Wizard tWizard) {
        OnEnterChild(tWizard);
        GameState.RunSpells();
    }

    public virtual void OnEnterChild(Wizard tWizard) {
        //empty
    }
}
}
