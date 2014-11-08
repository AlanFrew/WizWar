using System.Collections.Generic;

namespace WizWar1 {
class IItemUsage : IStackable, ISpellOrItemUsage {
    public IItemUsage(IItem tItem) {
        Item = tItem;
        EffectsWaiting = new List<Effect>();
    }

    public IItem Item { get; set; }

    public List<Effect> EffectsWaiting { get; set; } 
    
    public void OnRun() {
        //OnResolution();
        foreach (Effect e in EffectsWaiting) {
            e.OnRun();
        }
    }

    public override string ToString() {
        return Item + " used by " + Item.Controller;
    }
}
}
