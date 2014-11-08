namespace WizWar1 {
class Amplify : Spell {
    public Amplify() {
        Name = "Amplify";
        Description = "Double the total energy of a spell";
        ValidCastingTypes.Add(SpellType.Undef);
        ValidTargetTypes.Add(TargetTypes.Spell);
    }

    public override bool IsValidTarget(ITarget tTarget) {
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
