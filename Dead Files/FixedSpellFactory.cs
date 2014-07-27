using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    class FixedSpellFactory {
        public static ISpell makeCard(String tDescription) {
            if (tDescription == "Lightning Blast") {
                return new LightningBlast();
            }
            else if (tDescription == "Full Shield") {
                return new FullShield();
            }
            else if (tDescription == "Thornbush") {
                return new Thornbush();
            }
            else {
                throw new BadCardDescException();
            }
        }

        public void foo() {
            bool once = true;

            GotoLabel:
                System.Console.WriteLine("foo!");
                
                if (once == true) {
                    once = false;
                    goto GotoLabel;
                }
        }
    }
}
