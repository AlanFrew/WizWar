using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class Thornbush : Spell {
    public Thornbush() {
        Name = "Thornbush";
        validTargetTypes.Add(TargetTypes.Square);
        validCastingTypes.Add(SpellType.Neutral);
    }

    public override bool IsValidSpellTarget(ITarget tTarget, Wizard tCaster) {
        foreach (ICreation c in (tTarget as Square).creationsHere) {
            if (c is ThornbushCreation || c is SolidStone) {
                return false;
            }
        }

        return true;
    }

    public override void OnChildCast() {
        var temp = new MakeCreationEffect<ThornbushCreation>(new ThornbushCreation());
        MakeCreationEffect<ThornbushCreation> cte = Effect.Initialize<MakeCreationEffect<ThornbushCreation>>(Caster, this, SpellTarget, temp);
        EffectsWaiting.Add(cte);
    }

    public override void OnResolution() {
        var temp = effectsWaiting[0] as MakeCreationEffect<ThornbushCreation>;
        temp.myCreation.Initialize(temp.Caster, temp.target as Square);
    }
}
}
