namespace WizWar1 {
class BloodStoneItem : Stone, IListener<DamageEvent, Event> {
    public BloodStoneItem() {
        GameState.EventDispatcher.Register(this);
    }

    public void OnEvent(DamageEvent tEvent) {
        if (tEvent.myEffect.target == Carrier && tEvent.IsAttempt == true) {
            DamageEffect d = tEvent.myEffect;
            d.Amount -= 1;
            if (d.Amount < 0) {
                d.Amount = 0;
            }
        }
    }

    public override bool IsValidTarget(ITarget tTarget) {
        return false;
    }
}
}
