namespace WizWar1 {
class TakeDiscardEffect : Effect {
    ICard cardToRemove;
    public TakeDiscardEffect(ICard tCard) {
        cardToRemove = tCard;
    }

    public override void OnRunChild() {
        GameState.Discard.Remove(cardToRemove);
        Caster.giveCard(cardToRemove);
    }
}
}
