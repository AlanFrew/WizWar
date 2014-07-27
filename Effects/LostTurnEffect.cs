using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWar1 {
class LostTurnEffect : Effect, IListener<TurnStartEvent, Event> {
    public LostTurnEffect() {
        markers.Add(new DurationBasedMarker());
        GameState.eventDispatcher.Register(this);
    }

    //public override void OnRunChild() {
    //    (target as Wizard).EndTurn();
    //}

    public void OnEvent(TurnStartEvent tEvent) {
        if (tEvent.IsAttempt) {
            //tEvent.nextWizard = GameState.wizards[Library.IndexFixer(GameState.wizards.IndexOf(tEvent.nextWizard) + 1, GameState.wizards.Count)];
        }
        else {
            if (tEvent.nextWizard == target as Wizard) {
                GameState.eventDispatcher.Deregister(this);
                GameState.ActivePlayer.myUI.State = UIState.TurnComplete;
                //GameState.TurnCycle(); this seems to shortcut the process and keep the end turn button from going active
                MessageBox.Show("Lost turn due to Lightning Blast");
                //duration--; //most duration effects tick down on their
            }
        }
    }

    public override void OnRunChild() {
        //I hope leaving this empty doesn't set a bad precedent
    }

}
}
