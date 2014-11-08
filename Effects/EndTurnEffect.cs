namespace WizWar1 {
class EndTurnEffect : Effect {
    public override void OnRunChild() {
        if (target == GameState.ActivePlayer) {
            GameState.ActivePlayer.myUI.State = UIState.TurnComplete;
        }
    }
}
}
