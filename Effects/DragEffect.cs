using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class DragEffect : Effect {
    public override void OnRunChild() {
        (target as ILocatable).X = Caster.X;
        (target as ILocatable).Y = Caster.Y;
    }
}
}
