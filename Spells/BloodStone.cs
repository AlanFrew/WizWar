namespace WizWar1 {
class BloodStone : Spell {
    public BloodStone() {
        Name = "Bloodstone";
        Description = "Create a small ruby pendant that replaces lost blood, preventing 1 damage per attack";
        ValidCastingTypes.Add(SpellType.Item);
        ValidTargetTypes.Add(TargetTypes.None);
    }

    public override void OnChildCast() {
        EffectsWaiting.Add(Effect.New<CreateItemEffect<BloodStoneItem>>(Caster, this, SpellTarget));
    }
}
}