using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    class Error : Exception {
        public String explanation = "You have not selected a spell to cast";
    }
}
