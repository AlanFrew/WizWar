namespace WizWar1 {
class BrainStoneItem : Stone {
    public override void OnGainChild(Wizard tHolder) {
        tHolder.MaxHandSize += 2;
        GameState.Deck.DealCards(tHolder, 2);
    }

    public override void OnLossChild(Wizard tDropper) {
        tDropper.MaxHandSize -= 2;
        tDropper.SettleHand();
    }
}
}
