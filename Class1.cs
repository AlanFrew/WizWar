using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    class Box<T> where T : Item {

    }

    class Foo {
        void bar() {
            Box<Stone> blah = new Box<Stone>();
        }
    }
}
