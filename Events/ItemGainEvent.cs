using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class ItemGainEvent : Event {
    public IItem NewItem;

    public ItemGainEvent(IItem tNewItem) {
        NewItem = tNewItem;
    }
}
}
