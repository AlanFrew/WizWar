using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
interface ICardable : ITarget {
    string Name { get; set; }

    string Description { get; set; }

    ICard OriginalCard { get; set; }
}
}
