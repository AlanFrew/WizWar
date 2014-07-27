using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    class MultiplyPointBased : Effect {
        public override void OnRunChild() {
            foreach(Marker m in markers) {
                if (m is PointBasedMarker) {
                    (m as PointBasedMarker).PointValue *= 2;
                }
            }
        }
    }
}
