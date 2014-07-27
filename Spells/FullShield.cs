using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class FullShield : Spell {
    public FullShield() {
        Name = "Full Shield";
        validCastingTypes.Add(SpellType.Counteraction);
        validTargetTypes.Add(TargetTypes.Spell);
    }

    public override bool IsValidSpellTarget(ITarget tTarget, Wizard tCaster) {
        //A spell that targets the caster can be stopped completely; otherwise there is a second opportunity to counteract part of the spell when the effects are pushed
        if ((tTarget as Spell).SpellTarget == tCaster) {
            if((tTarget as Spell).ActiveSpellType == SpellType.Attack) {
                return true;
            }
        }
        else {
            //foreach (Effect e in (tTarget as ISpell).EffectsWaiting) {
            //    if (e.target == Caster) {
            //        return true;
            //    }
            //}
        }

        return false;
    }

    public override void OnChildCast() {
        if ((SpellTarget as ISpell).SpellTarget == Caster) {
            EffectsWaiting.Add(Effect.New<CounterSpellEffect>(Caster, this, SpellTarget));
        }
        else {
            //foreach (Effect e in GameState.instantEffectStack) {
            //    if (e.target == Caster && e.source == SpellTarget) {
            //        EffectsWaiting.Add(Effect.New<FullShieldEffect>(Caster, this, e));
            //    }
            //}
            //foreach (Effect e in GameState.durationEffects) {
            //    if (e.target == Caster && e.source == SpellTarget) {
            //        EffectsWaiting.Add(Effect.New<FullShieldEffect>(Caster, this, e));
            //    }
            //}
        }
    }
}
}
