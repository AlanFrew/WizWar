namespace WizWar1 {
class Blind : NumberedSpell {
    public Blind() {
        Name = "Blind";
        Description = "Sear an opponents eyes, causing him to stagger blindly and misdirect his spells";
        ValidCastingTypes.Add(SpellType.Attack);
        ValidTargetTypes.Add(TargetTypes.Wizard);
        Markers.Add(new DurationBasedMarker());
        //AcceptsNumber = true;
    }

    public override bool IsValidTarget(ITarget tTarget) {
        if (tTarget is Wizard) {
            return true;
        }
        return false;
    }

    public override void OnChildCast() {
        var b = Effect.New<BlindEffect>(Caster, this, SpellTarget);
        EffectsWaiting.Add(b);
    }

    public override void OnResolution() {
        base.OnResolution();
    }
}
}
