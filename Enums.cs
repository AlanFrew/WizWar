using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    public enum Direction {
        //yes, it is important that North is 0 so that a Direction can be an array index
        North = 0, South, East, West
    }

    public class DirectionC {
        public static Direction oppositeDirection(Direction tDirection) {
            switch (tDirection) {
                case Direction.West:
                    return Direction.East;
                case Direction.East:
                    return Direction.West;
                case Direction.North:
                    return Direction.South;
                case Direction.South:
                    return Direction.North;
                default:
                    throw new UnreachableException();
            }
        }
        public static int nDirections = 4;
    }

    public enum MapReadMode {
        Doors, Walls, Treasures, Bases
    }

    public enum UIState {
        Normal, CastingSpell, TestingLoS, Querying, QueryingSquare, FindingTarget, ConfirmingTarget, CastQuery, 
        AddingNumber, Locked, Drawing, Discarding, TurnComplete, Previous, Undef
    }

    public enum Redirect {
        Halt, Proceed, Skip
    }

    public enum RedirectSet {
        Halt, Allow, Deny, NoChange
    }

    public enum ControlFlowState {
        FindTarget, Undef
    }

    public enum DamageType {
        Physical, Magical, Undef
    }

    public enum TargetTypes {
        Square, WallSpace, Wall, Creation, Wizard, Self, Card, Effect, Spell, Item, Treasure, None, Undef
    }

    public enum SpellType {
        Attack, Neutral, Counteraction, Item, Undef
    }

    public enum PieceType {
        Creation, Effect, Item, Spell, Square, Wall, Wizard, Undef
    }

    public enum WizardName {
        BlueWizard = 0, RedWizard, GreenWizard, YellowWizard
    }

    public enum CardSource {
        Player, Deck, Discard
    }
}
