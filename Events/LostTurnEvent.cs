using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    class LostTurnEvent : Event {
        public Wizard target;
        public int duration;
        public Wizard controller;

        public LostTurnEvent(Wizard tTarget, int tDuration, Wizard tController) {
            target = tTarget;
            duration = tDuration;
            controller = tController;
        }
    }
}
