using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class ItemRevealEvent : Event {
    public ItemCard NewItem;

    public ItemRevealEvent(ItemCard tNewItem) {
        NewItem = tNewItem;
    }
}
}
