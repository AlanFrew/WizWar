using System;

namespace WizWar1 {
    public enum Direction {
        //yes, it is important that North is 0 so that a Direction can be an array index
        North = 0, South, East, West
    }

    internal class DirectionC {
        internal static Direction oppositeDirection(Direction tDirection) {
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
        internal static int nDirections = 4;

        internal static Direction AngleToDirection(double angle) {
            var reducedAngle = angle % 2 * Math.PI;

            var roundedAngle = (int)Math.Round(reducedAngle / (Math.PI / 2));

            switch (roundedAngle) {
                case 0: return Direction.East;
                case 1: return Direction.North;
                case 2: return Direction.West;
                case 3: return Direction.South;
                default:
                    throw new UnreachableException();
            }
        }

        internal static Direction DirectionToTarget(ILocatable source, ILocatable target) {
            var xChange = source.X - target.X;
            var yChange = source.Y - target.Y;

            if (xChange == 0 && yChange == 0) {
                throw new Exception("Direction is undefined because the points are the same");
            }

            if (Math.Abs(xChange) > Math.Abs(yChange)) {
                if (xChange > 0) {
                    return Direction.West;
                }

                return Direction.East;
            }

            if (yChange > 0) {
                return Direction.South;
            }

            return Direction.North;
        }   
    }

    public enum MapReadMode {
        Doors, Walls, Treasures, Bases
    }

    public enum UIState {
        Normal, CastingSpell, TestingLoS, Querying, QueryingSquare, UsingItem, ConfirmingTarget, CastQuery, 
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
        Square, WallSpace, Wall, Creation, Wizard, Self, Card, Effect, Spell, Item, Treasure, Door, None, Undef
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
