using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    interface ITarget {
        TargetTypes ActiveTargetType {
            get;
        }

        bool IsTargetableAs(TargetTypes tTargetType);

        void BecomeTarget(TargetTypes tActiveSpellType);

        void RanChild(Effect tEffect);
    }
}
