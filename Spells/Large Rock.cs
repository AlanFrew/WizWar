namespace WizWar1 {
class LargeRock : Spell {
    public LargeRock() {
        Name = "Large Rock";
        Description = "Throw a large rock for 3 damage";
        ValidTargetTypes.Add(TargetTypes.None);
        ValidCastingTypes.Add(SpellType.Item);
    }

    public override bool IsValidTarget(ITarget tTarget) {
        return false;
    }

    public override void OnChildCast() {
        EffectsWaiting.Add(Effect.New<CreateItemEffect<LargeRockItem>>(Caster, this, SpellTarget));
    }
}
}