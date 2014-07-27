using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class Amplify : Spell {
    public Amplify() {
        Name = "Amplify";
        validCastingTypes.Add(SpellType.Undef);
        validTargetTypes.Add(TargetTypes.Spell);
    }

    public override bool IsValidSpellTarget(ITarget tTarget, Wizard tCaster) {
        foreach (Marker m in (tTarget as ISpell).Markers) {
            if (m is PointBasedMarker) {
                return true;
            }
        }

        return false;
    }

    public override void OnChildCast() {
        EffectsWaiting.Add(Effect.New<MultiplyPointBased>(Caster, this, SpellTarget));
    }

}
}
