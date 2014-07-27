using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;

namespace WizWar1 {
abstract class Marker : ICopiable<Marker> {
    public virtual Marker RecursiveCopy() {
        return (System.Activator.CreateInstance(this.GetType()) as Marker);
    }
}
}
