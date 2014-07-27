using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class Buddy : Spell {
    public Buddy() {
        Name = "Buddy";
        validCastingTypes.Add(SpellType.Neutral);
        validTargetTypes.Add(TargetTypes.Wizard);
    }

    //public override bool IsValidSpellTarget(ITarget tTarget) {
    //    return base.IsValidSpellTarget(tTarget);
    //}

    public override void OnChildCast() {
        EffectsWaiting.Add(Effect.New<BuddyEffect>(Caster, this, SpellTarget));
    }
}
}
