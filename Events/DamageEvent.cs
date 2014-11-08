namespace WizWar1 {
class DamageEvent : Event {
    internal DamageEffect myEffect;
    public DamageEvent(DamageEffect tEffect) {
        myEffect = tEffect;
    }
}
}
