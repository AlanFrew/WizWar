using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class CounterSpellEffect : Effect {
    public override void OnRunChild() {
        GameState.theStack.Remove(target as IStackable);
    }
}
}
