using System.Windows.Forms;

namespace WizWar1 {
class InvisibleEffect : Effect, IListener<CastEvent, Event>
{
    public InvisibleEffect() {
        markers.Add(new DurationBasedMarker());
    }

    public void OnEvent(CastEvent tEvent) {
        ISpell s = tEvent.SpellBeingCast;

        if (s.SpellTarget != Caster) {
            return;
        }

        if (GameState.Dice.Next(1, 4) != 1) {         
            foreach (Effect e in s.EffectsWaiting) {
                GameState.KillEffect(e);
            }

            MessageBox.Show("The invisibility caused you to miss, and the spell fizzled without a target.");
        }  
    }

    public override void OnRunChild() {
        GameState.EventDispatcher.Register(this);
    }
}
}
