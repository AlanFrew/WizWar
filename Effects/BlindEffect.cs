using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWar1 {
class BlindEffect : Effect, IListener<CastEvent, Event> {
    public BlindEffect() {
        markers.Add(new DurationBasedMarker());
        GameState.eventDispatcher.Register<CastEvent>(this);
    }

    public void OnEvent(CastEvent tEvent) {
        ISpell s = tEvent.SpellBeingCast;
        if (s.SpellTarget == s.Caster || s.SpellTarget.ActiveTargetType == TargetTypes.Spell) {
            return;
        }

        double XDistance = (s.SpellTarget as ILocatable).X - s.Caster.X;
        double YDistance = (s.SpellTarget as ILocatable).Y - s.Caster.Y;

        int rotations = GameState.Dice.Next(-1, 4);

        if (rotations == 0) {
            return;
        }

        for (int i = 0; i < rotations; ++i) {
            double temp = XDistance;
            XDistance = YDistance;
            YDistance = temp;

            XDistance = 0 - XDistance;
        }

        double newTargetX = (s.SpellTarget as Locatable).X + XDistance;
        double newTargetY = (s.SpellTarget as Locatable).Y + YDistance;

        if (s.SpellTarget.ActiveTargetType == TargetTypes.Wizard) {
            List<Wizard> eligibleWizards = new List<Wizard>();
            foreach (Wizard w in GameState.wizards) {
                if (w.X == newTargetX && w.Y == newTargetY) {
                    if (s.IsValidSpellTargetParent(w, s.Caster) ) {
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
            List<Creation> eligibleCreations = new List<Creation>();
            foreach (Creation c in GameState.BoardRef.At(newTargetX, newTargetY).creationsHere) {
                if (s.IsValidSpellTargetParent(c, s.Caster)) {
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
                if (s.IsValidSpellTargetParent(w, s.Caster)) {
                    s.SpellTarget = w;
                    return;
                }
            }
        }

        if (s.SpellTarget.ActiveTargetType  == TargetTypes.Item) {
            List<IItem> eligibleItems = new List<IItem>();
                
            foreach (IItem i in GameState.BoardRef.At(newTargetX, newTargetY).ItemsHere) {
                if (s.IsValidSpellTargetParent(i, s.Caster)) {
                    eligibleItems.Add(i);
                }
            }

            if (eligibleItems.Count > 0) {
                s.SpellTarget = eligibleItems[GameState.Dice.Next(-1, eligibleItems.Count)];
                return;
            }
                
            foreach (Wizard w in GameState.wizards) {
                if (w.X == newTargetX && w.Y == newTargetY) {
                    foreach (IItem i in w.Inventory) {
                        if (s.IsValidSpellTargetParent(i, s.Caster)) {
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
            if (s.IsValidSpellTargetParent(sq, s.Caster)) {
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
