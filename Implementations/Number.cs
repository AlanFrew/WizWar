using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    class Number : Card {
        private int value;
        public int Value {
            get {
                return value;
            }
        }

        public Number(int tValue) {
            value = tValue;
            name = value.ToString();
        }
    }
}
