using System.Collections.Generic;

namespace WizWar1 {
interface ISpellOrItemUsage {
    List<Effect> EffectsWaiting { get; set; }
}
}
