using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class FullReflection : Spell {
    public FullReflection() {
        Name = "Full Reflection";
        validCastingTypes.Add(SpellType.Counteraction);
        validTargetTypes.Add(TargetTypes.Spell);
    }

    public override bool IsValidSpellTarget(ITarget tTarget, Wizard tCaster) {
        if ((tTarget as Spell).SpellTarget == tCaster) {
            if ((tTarget as Spell).ActiveSpellType == SpellType.Attack) {
                return true;
            }
        }
        else {
            foreach (Effect e in (tTarget as ISpell).EffectsWaiting) {
                if (e.target == Caster) {
                    return true;
                }
            }
        }

        return false;
    }

    public override void OnChildCast() {
        if (SpellTarget is Spell) {
            Spell temp = SpellTarget as Spell;
            if (temp.SpellTarget == Caster) {
                EffectsWaiting.Add(Effect.Initialize<AlterSpellEffect>(Caster, this, SpellTarget, new AlterSpellEffect(temp.GetType().GetProperty("SpellTarget"), temp.Caster)));
                EffectsWaiting.Add(Effect.Initialize<AlterSpellEffect>(Caster, this, SpellTarget, new AlterSpellEffect(temp.GetType().GetProperty("Caster"), Caster)));
            }
        }
        else if (SpellTarget is Effect) {
            throw new NotImplementedException();
        }
        //if ((SpellTarget as ISpell).SpellTarget == Caster) {
        //    EffectsWaiting.Add(Effect.New<FullShieldTotalEffect>(Caster, this, SpellTarget));
        //}
        //else {
        //    foreach (Effect e in GameState.instantEffectStack) {
        //        if (e.target == Caster && e.source == SpellTarget) {
        //            EffectsWaiting.Add(Effect.New<FullShieldEffect>(Caster, this, e));
        //        }
        //    }
        //    foreach (Effect e in GameState.durationEffects) {
        //        if (e.target == Caster && e.source == SpellTarget) {
        //            EffectsWaiting.Add(Effect.New<FullShieldEffect>(Caster, this, e));
        //        }
        //    }
        //}

        //if (SpellTarget is ISpell) {
        //    ISpell s = (SpellTarget as ISpell).RecursiveCopy();
        //    s.SpellTarget = s.Caster;
        //    GameState.NewSpell(s);
        //}
        //else if (SpellTarget is Effect) {
        //    Effect e = (SpellTarget as Effect).RecursiveCopy();
        //    e.target = e.Caster;
        //    GameState.NewEffect(e);
        //}
    }
}
}
