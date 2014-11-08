using System;
using Library;

namespace WizWar1 {
abstract class Marker : ICopiable<Marker> {
    public virtual Marker RecursiveCopy() {
        return (Activator.CreateInstance(GetType()) as Marker);
    }
}
}
