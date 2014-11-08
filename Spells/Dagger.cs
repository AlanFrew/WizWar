namespace WizWar1 {
class Dagger : Spell {
    public Dagger() {
        Name = "Dagger";
        Description = "You may throw it";

        ValidCastingTypes.Add(SpellType.Item);
        ValidTargetTypes.Add(TargetTypes.None);
    }

    public override bool IsValidTarget(ITarget tTarget) {
        return false;
    }
    public override void OnChildCast() {
        EffectsWaiting.Add(Effect.Initialize<CreateItemEffect<DaggerItem>>(Caster, this, SpellTarget, new CreateItemEffect<DaggerItem>()));
    }
}
}
