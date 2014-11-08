using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace WizWar1 {
class Cardable : Targetable, ICardable {
    public string Name { get; set; }

    public string Description { get; set; }

    public ICard OriginalCard { get; set; }
}
}
