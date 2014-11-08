namespace WizWar1 {
class DispelCreation : Spell {
    public DispelCreation() {
        Name = "Dispel Creation";
        Description = "Unstitch the theads of magic holding a creation together, causing it to collapse into dust";
        ValidCastingTypes.Add(SpellType.Neutral);
        ValidTargetTypes.Add(TargetTypes.Wall);
    }

    public override void OnChildCast() {
        EffectsWaiting.Add(Effect.New<DispelCreationEffect>(Caster, this, SpellTarget));
    }

    public override void OnResolution() {
        base.OnResolution();
    }
}
}
