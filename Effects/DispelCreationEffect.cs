using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class DispelCreationEffect : Effect {
    public override void OnRunChild() {
        if (target is IWall) {
            GameState.BoardRef.RemoveWall(target as IWall);
        }
        else {
            GameState.BoardRef.At((target as Creation).X, (target as Creation).Y).creationsHere.Remove(target as Creation);
        }
    }
}
}
