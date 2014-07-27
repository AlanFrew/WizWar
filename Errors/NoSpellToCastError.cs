using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    class NoSpellToCastError : Error {
        public NoSpellToCastError() {
            explanation = "You have not selected a spell to cast";
        }
    }
}
