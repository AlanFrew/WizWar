namespace WizWar1 {
class DaggerItem : Item {
    public DaggerItem() {
        ValidTargetTypes.Add(TargetTypes.Wizard);
        ValidTargetTypes.Add(TargetTypes.Wall);
        ValidTargetTypes.Add(TargetTypes.Creation);
        ValidTargetTypes.Add(TargetTypes.Door);
    }

    public override void OnActivationChild() {
        Carrier.myUI.State = UIState.UsingItem;
    }

    public override IItemUsage UseItem() {
        var iu = new IItemUsage(this);

        var ite = new ItemThrowEffect(this, Target as Wizard);

        iu.EffectsWaiting.Add(Effect.Initialize<ItemThrowEffect>(Carrier, this, ItemTarget, ite));
        iu.EffectsWaiting.Add(Effect.Initialize<DamageEffect>(Carrier, this, ItemTarget, new DamageEffect(3, DamageType.Physical)));

        return iu;
    }
}
}
