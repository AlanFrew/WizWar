namespace WizWar1 {
class ForceDiscardEffect : Effect {
    public ICard NamedCard;

    public void Initialize(ICard tNamedCard) {
        NamedCard = tNamedCard;
        
    }

    public override void OnRunChild() {
        foreach (ICard c in (target as Wizard).Hand) {
            if (c.GetType() == NamedCard.GetType()) {
                (target as Wizard).TakeCard(c);
                return;
            }
        }
    }
}
}
