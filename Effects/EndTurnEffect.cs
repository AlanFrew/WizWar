using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class EndTurnEffect : Effect {
    public override void OnRunChild() {
        if (target == GameState.ActivePlayer) {
            GameState.ActivePlayer.myUI.State = UIState.TurnComplete;
        }
    }
}
}
