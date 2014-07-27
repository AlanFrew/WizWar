using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class AntiAnti : Spell {
    public AntiAnti() {
        Name = "Anti-Anti";
        validCastingTypes.Add(SpellType.Counteraction);
        validTargetTypes.Add(TargetTypes.Spell);
    }

    public override bool IsValidSpellTarget(ITarget tTarget, Wizard tCaster) {
        if ((tTarget as ISpell).SpellTarget == tCaster) {
            if ((tTarget as ISpell).Caster != tCaster) {
                if ((tTarget as ISpell).ActiveSpellType == SpellType.Counteraction) {
                    return true;
                }
            }
        }
        return false;
    }

    public override void OnChildCast() {
        EffectsWaiting.Add(Effect.New<NullifyEffect>(Caster, this, SpellTarget));
    }


}
}
