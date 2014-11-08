namespace WizWar1 {
class FullShield : Spell {
    public FullShield() {
        Name = "Full Shield";
        Description = "A powerful magic shield counters an incoming spell";
        ValidCastingTypes.Add(SpellType.Counteraction);
        ValidTargetTypes.Add(TargetTypes.Spell);
    }

    public override bool IsValidTarget(ITarget tTarget) {
        //A spell that targets the caster can be stopped completely; otherwise there is a second opportunity to counteract part of the spell when the effects are pushed
        if ((tTarget as Spell).SpellTarget == Caster) {
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
            if (GameState.Dice.Next(1, 2) == 1) {
                EffectsWaiting.Add(Effect.New<CounterSpellEffect>(Caster, this, SpellTarget));
            }
        }
    }
}
}
