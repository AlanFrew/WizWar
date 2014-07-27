using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class ForceMoveEffect : Effect {
    public delegate void ForceMoveDelegate(Wizard tWizard);
    ForceMoveDelegate ForceMoveMethod;

    public override void OnRunChild() {
        ForceMoveMethod(target as Wizard);
    }

    public void Initialize(ForceMoveDelegate tDelegate) {
        ForceMoveMethod = tDelegate;
    }
}
}
