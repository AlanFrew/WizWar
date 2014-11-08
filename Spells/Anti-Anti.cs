namespace WizWar1 {
class AntiAnti : Spell {
    public AntiAnti() {
        Name = "Anti-Anti";
        Description = "Counter a counterspell that targets an attack";
        ValidCastingTypes.Add(SpellType.Counteraction);
        ValidTargetTypes.Add(TargetTypes.Spell);
    }

    public override bool IsValidTarget(ITarget tTarget) {
        var spell = tTarget as ISpell;
        if (spell.SpellTarget == Caster) {
            if (spell.Caster != Caster) {
                if (spell.ActiveSpellType == SpellType.Counteraction) {
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
