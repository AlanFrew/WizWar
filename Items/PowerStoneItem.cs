namespace WizWar1 {
class PowerStoneItem : Stone, IListener<CastEvent, Event>{
    public PowerStoneItem() {
        GameState.EventDispatcher.Register(this);
    }

    public void OnEvent(CastEvent tEvent) {
        if (tEvent.Source == Carrier) {
            if (tEvent.SpellBeingCast is INumberable) {
                (tEvent.SpellBeingCast as INumberable).CardValue++;
            }
        }
    }

    public override bool IsValidTarget(ITarget tTarget) {
        return false;
    }
}
}
