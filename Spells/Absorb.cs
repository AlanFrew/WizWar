using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class Absorb : Spell {
    public Absorb() {
        Name = "Absorb";
        validCastingTypes.Add(SpellType.Counteraction);
        validTargetTypes.Add(TargetTypes.Effect);
        markers.Add(new PointBasedMarker(3));
    }

    public override bool IsValidSpellTarget(ITarget tTarget, Wizard tCaster) {
        if ((tTarget as Effect).target == tCaster) {
            foreach (Marker m in (tTarget as Effect).markers) {
                if (m is PointBasedMarker) {
                    return true;
                }
            }
        }

        return false;
    }

    public override void OnChildCast() {
        EffectsWaiting.Add(Effect.New<AbsorbEffect>(Caster, this, SpellTarget));
    }
}
}
