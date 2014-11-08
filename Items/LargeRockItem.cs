namespace WizWar1 {
    class LargeRockItem : Item {
        public LargeRockItem() {
            ValidTargetTypes.Add(TargetTypes.Door);
            ValidTargetTypes.Add(TargetTypes.Wall);
            ValidTargetTypes.Add(TargetTypes.Wizard);
            ValidTargetTypes.Add(TargetTypes.Square);
        }
        public override void OnActivationChild() {
            Carrier.myUI.State = UIState.UsingItem;
        }

        public override IItemUsage UseItem() {
            var iu = new IItemUsage(this);

            var ite = new ItemThrowEffect(this, ItemThrowEffect.StayInSquare(this, Target));

            iu.EffectsWaiting.Add(Effect.Initialize<ItemThrowEffect>(Carrier, this, ItemTarget, ite));
            iu.EffectsWaiting.Add(Effect.Initialize<DamageEffect>(Carrier, this, ItemTarget, new DamageEffect(3, DamageType.Physical)));

            return iu;
        }
    }
}