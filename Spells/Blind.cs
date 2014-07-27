using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class Blind : Spell {
    public Blind() {
        Name = "Blind";
        validCastingTypes.Add(SpellType.Attack);
        validTargetTypes.Add(TargetTypes.Wizard);
        markers.Add(new DurationBasedMarker());
        acceptsNumber = true;
    }

    public override bool IsValidSpellTarget(ITarget tTarget, Wizard tCaster) {
        if (tTarget is Wizard) {
            return true;
        }
        return false;
    }

    public override void OnChildCast() {
        BlindEffect b = Effect.New<BlindEffect>(Caster, this, SpellTarget);
        EffectsWaiting.Add(b);
    }

    public override void OnResolution() {
        base.OnResolution();
    }
}
}
