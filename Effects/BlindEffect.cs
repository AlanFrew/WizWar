using System.Collections.Generic;
using System.Windows.Forms;

namespace WizWar1 {
class BlindEffect : Effect, IListener<CastEvent, Event> {
    public BlindEffect() {
        markers.Add(new DurationBasedMarker());
        GameState.EventDispatcher.Register<CastEvent>(this);
    }

    public void OnEvent(CastEvent tEvent) {
        ISpell s = tEvent.SpellBeingCast;
        if (tEvent.SpellBeingCast.Caster != target || s.SpellTarget == s.Caster || s.SpellTarget.ActiveTargetType == TargetTypes.Spell) {
            return;
        }

        double xDistance = (s.SpellTarget as ILocatable).X - s.Caster.X;
        double yDistance = (s.SpellTarget as ILocatable).Y - s.Caster.Y;

        int rotations = GameState.Dice.Next(0, 4);

        if (rotations == 0) {
            MessageBox.Show("Despite being blinded, you managed to hit your target");

            return;
        }

        for (int i = 0; i < rotations; ++i) {
            double temp = xDistance;
            xDistance = yDistance;
            yDistance = temp;

            xDistance = 0 - xDistance;
        }

        double newTargetX = (s.SpellTarget as Locatable).X + xDistance;
        double newTargetY = (s.SpellTarget as Locatable).Y + yDistance;

        if (s.SpellTarget.ActiveTargetType == TargetTypes.Wizard) {
            var eligibleWizards = new List<Wizard>();
            foreach (Wizard w in GameState.Wizards) {
                if (w.X == newTargetX && w.Y == newTargetY) {
                    if (s.IsValidTargetParent(w) ) {
                        eligibleWizards.Add(w);
                    }
                }
            }
                
            if (eligibleWizards.Count > 0) {
                s.SpellTarget = eligibleWizards[GameState.Dice.Next(-1, eligibleWizards.Count)];
                return;
            }
        }

        if (s.SpellTarget.ActiveTargetType == TargetTypes.Creation) {
            var eligibleCreations = new List<ICreation>();
            foreach (ICreation c in GameState.BoardRef.At(newTargetX, newTargetY).creationsHere) {
                if (s.IsValidTargetParent(c)) {
                    eligibleCreations.Add(c);
                }
            }

            if (eligibleCreations.Count > 0) {
                s.SpellTarget = eligibleCreations[GameState.Dice.Next(-1, eligibleCreations.Count)];
                return;
            }
        }

        if (s.SpellTarget.ActiveTargetType == TargetTypes.Wall) {
            IWall w = GameState.BoardRef.LookForWall(newTargetX, newTargetY);

            if ( w != null) {
                if (s.IsValidTargetParent(w)) {
                    s.SpellTarget = w;
                    return;
                }
            }
        }

        if (s.SpellTarget.ActiveTargetType  == TargetTypes.Item) {
            var eligibleItems = new List<ILocatable>();
                
            foreach (IItem i in GameState.BoardRef.At(newTargetX, newTargetY).ItemsHere) {
                if (s.IsValidTargetParent(i)) {
                    eligibleItems.Add(i);
                }
            }

            if (eligibleItems.Count > 0) {
                s.SpellTarget = eligibleItems[GameState.Dice.Next(-1, eligibleItems.Count)];
                return;
            }
                
            foreach (Wizard w in GameState.Wizards) {
                if (w.X == newTargetX && w.Y == newTargetY) {
                    foreach (IItem i in w.Inventory) {
                        if (s.IsValidTargetParent(i)) {
                            eligibleItems.Add(i);
                        }
                    }
                }
            }
   
            if (eligibleItems.Count > 0) {
                s.SpellTarget = eligibleItems[GameState.Dice.Next(-1, eligibleItems.Count)];
                return;
            }
        }

        if (s.ActiveTargetType == TargetTypes.Square) {
            Square sq = GameState.BoardRef.At(newTargetX, newTargetY);
            if (s.IsValidTargetParent(sq)) {
                s.SpellTarget = sq;
                return;
            }
        }

        MessageBox.Show("The blindness caused you to miss, and the spell fizzled without a target.");
    }

    public override void OnRunChild() {
        //empty
    }
} 
}
