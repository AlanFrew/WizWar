namespace WizWar1 {
class Shrink : Spell {
    public Shrink() {
        Name = "Shrink";
        ValidCastingTypes.Add(SpellType.Counteraction);
        ValidTargetTypes.Add(TargetTypes.Spell);
    }

    public override bool IsValidTarget(ITarget tTarget) {          
        if ((tTarget as Spell).SpellTarget == Caster) {
            if ((tTarget as Spell).ActiveSpellType == SpellType.Attack) {
                return true;
            }
        }

        return false;
    }
}
}
