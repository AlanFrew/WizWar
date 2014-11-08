namespace WizWar1 {
class Absorb : Spell {
    public Absorb() {
        Name = "Absorb";
        Description = "Create a magical shield that prevents the next 3 damage dealt to you";
        ValidCastingTypes.Add(SpellType.Counteraction);
        ValidTargetTypes.Add(TargetTypes.Effect);
        Markers.Add(new PointBasedMarker(3));
    }

    public override bool IsValidTarget(ITarget tTarget) {
        if ((tTarget as Effect).target == Caster) {
            foreach (Marker m in (tTarget as Effect).markers) {
                if (m is PointBasedMarker) {
                    return true;
                }
            }
        }

        return false;
    }

    public override void OnChildCast() {
        EffectsWaiting.Add(Effect.New<AbsorbEffect>(Caster, this, SpellTarget));
    }
}
}
