using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    class Invisible : Spell {
        public Invisible() {
            Name = "Invisible";
            validTargetTypes.Add(TargetTypes.Wizard);
            acceptsNumber = true;
        }
    }
}
