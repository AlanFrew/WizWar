using System.Windows.Forms;

namespace WizWar1 {
class LostTurnEffect : Effect, IListener<TurnStartEvent, Event> {
    public LostTurnEffect() {
        markers.Add(new DurationBasedMarker());
        GameState.EventDispatcher.Register(this);
    }

    public void OnEvent(TurnStartEvent tEvent) {
        if (tEvent.IsAttempt) {
            //tEvent.nextWizard = GameState.wizards[Library.IndexFixer(GameState.wizards.IndexOf(tEvent.nextWizard) + 1, GameState.wizards.Count)];
        }
        else {
            if (tEvent.NextWizard == target as Wizard) {
                GameState.EventDispatcher.Deregister(this);
                GameState.ActivePlayer.myUI.State = UIState.TurnComplete;
                MessageBox.Show("Lost turn due to " + source);
                //duration--; //most duration effects tick down on their
            }
        }
    }

    public override void OnRunChild() {
        //I hope leaving this empty doesn't set a bad precedent
    }
}
}
