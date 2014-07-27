using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class BloodStoneItem : Stone, IListener<DamageEvent, Event> {
    public BloodStoneItem() {
        GameState.eventDispatcher.Register(this);
    }

    public void OnEvent(DamageEvent tEvent) {
        if (tEvent.myEffect.target == Carrier && tEvent.IsAttempt == true) {
            DamageEffect d = tEvent.myEffect;
            d.Amount -= 1;
            if (d.Amount < 0) {
                d.Amount = 0;
            }
        }
    }

    public override bool IsValidTargetForItem(ITarget tTarget) {
        return false;
    }
}
}
