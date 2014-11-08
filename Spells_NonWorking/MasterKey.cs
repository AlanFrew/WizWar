namespace WizWar1 {
class MasterKey : Spell {
    public MasterKey() {
        Name = "Master Key";
        Description = "Create a magical key that will open any undamaged door";
        ValidCastingTypes.Add(SpellType.Item);
        ValidTargetTypes.Add(TargetTypes.None);
    }

    public override void OnChildCast() {
        EffectsWaiting.Add(Effect.New<CreateItemEffect<MasterKeyItem>>(Caster, this, SpellTarget));
    }
}
}