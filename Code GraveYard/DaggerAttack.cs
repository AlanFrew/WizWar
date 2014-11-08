using System;

namespace WizWar1 {
class DaggerAttack : Effect {
    public DaggerAttack() {

    }

    public override void OnRunChild() {
        (source as Item).Carrier.loseItem(source as Item);
        
        if (target is IWall) {
            if ((target as IWall).IsVertical()) {
                if ((target as IWall).X < (source as Item).Carrier.X) {
                    GameState.BoardRef.At(Math.Ceiling((target as IWall).X), (target as IWall).Y).AddLocatable(source as Item);
                }
                else {
                    GameState.BoardRef.At(Math.Floor((target as IWall).X), (target as IWall).Y).AddLocatable(source as Item);
                }
            }
            else {
                if ((target as IWall).Y < (source as Item).Carrier.Y) {
                    GameState.BoardRef.At((target as IWall).X, Math.Ceiling((target as IWall).Y)).AddLocatable(source as Item);
                }
                else {
                    GameState.BoardRef.At((target as IWall).X, Math.Floor((target as IWall).Y)).AddLocatable(source as Item);
                }
            }
        }
        else {
            GameState.BoardRef.At((target as ILocatable).X, (target as ILocatable).Y).AddLocatable(source as Item);
        }
    }
}
}
