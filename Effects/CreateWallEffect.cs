using System;

namespace WizWar1 {
class CreateWallEffect : Effect {
    public override void OnRunChild() {
        var ws = target as WallSpace;
        var c = new IllusionWallCreation {Creator = Caster, X = ws.X, Y = ws.Y};

        if (c.X != Math.Floor(c.X)) { //is vertical
            //MessageBox.Show(" " + GameState.BoardRef.At(c.X , Math.Floor(c.Y)).X);
            //MessageBox.Show(" " + GameState.BoardRef.At(c.X, Math.Floor(c.Y)).Y);
            //MessageBox.Show(" " + GameState.BoardRef.At(c.X + 1, Math.Ceiling(c.Y)).X);
            //MessageBox.Show(" " + GameState.BoardRef.At(c.X + 1, Math.Ceiling(c.Y)).Y);
            c.FirstNeighbor = GameState.BoardRef.At(c.X, Math.Floor(c.Y));
            c.SecondNeighbor = GameState.BoardRef.At(c.X + 1, Math.Floor(c.Y));            
        }
        else {
            c.FirstNeighbor = GameState.BoardRef.At(Math.Floor(c.X), c.Y);
            c.SecondNeighbor = GameState.BoardRef.At(Math.Floor(c.X), c.Y + 1);
        }

        GameState.BoardRef.AddWall(c);
    }
}
}
