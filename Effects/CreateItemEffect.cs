using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class CreateItemEffect<T> : Effect where T : IItem, new() {
    public T ItemToCreate;

    public void Initialize() {
        ItemToCreate = new T();
    }

    public override void OnRunChild() {
        (Caster as Wizard).giveItem(ItemToCreate);
    }
}
}
