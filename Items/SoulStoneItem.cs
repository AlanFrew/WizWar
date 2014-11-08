namespace WizWar1 {
class SoulStoneItem : Stone, IListener<DamageEvent, Event>{
    public SoulStoneItem() {
        GameState.EventDispatcher.Register(this);
    }

    public void OnEvent(DamageEvent tEvent) {
        if (tEvent.myEffect.target == Carrier && tEvent.IsAttempt == true) {
            DamageEffect d = tEvent.myEffect;

            if (d.DamageType == DamageType.Magical) {
                var newLifeTotal = (d.target as Wizard).hit_points - d.Amount;
                if (newLifeTotal < 3) {
                    d.Amount -= 3 - newLifeTotal;
                }
            }
        }
    }

    public override bool IsValidTarget(ITarget tTarget) {
        return false;
    }
}
}
