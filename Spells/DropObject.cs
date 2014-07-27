using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class DropObject : Spell {
    public DropObject() {
        Name = "Drop Object";
        validTargetTypes.Add(TargetTypes.Item);
    }

    public override bool IsValidSpellTarget(ITarget tTarget, Wizard tCaster) {
        //AllowTargeting a = new AllowTargeting((tTarget as IItem).Carrier); //not sure this class is valid anymore
        if (GameState.InitialUltimatum(Event.New<TargetingEvent>(true, new TargetingEvent((tTarget as IItem).Carrier))) == Redirect.Proceed) {
            return true;
        }

        return false;
    }

    public override void OnChildCast() {
        EffectsWaiting.Add(Effect.New<DropObjectEffect>(Caster, this, SpellTarget));
    }
}
}