using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class DestroyWallEffect : Effect {
    public override void OnRunChild() {
        GameState.eventDispatcher.Notify(new DestroyObjectEvent(target));
    }
}
}
