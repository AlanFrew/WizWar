using System.Linq;

namespace WizWar1 {
internal class WallSpace : Targetable {
    public double X { get; set; }

    public double Y { get; set; }

    public WallSpace(double tX, double tY) {
        X = tX;
        Y = tY;

        activeTargetType = TargetTypes.WallSpace;
    }

    public IWall WallHere {
        get {
            return GameState.BoardRef.Walls.Values.FirstOrDefault(wall => wall.X == X && wall.Y == Y);
        }
    }
}
}
