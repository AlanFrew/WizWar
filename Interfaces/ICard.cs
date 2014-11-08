using System;
using System.Windows.Forms.VisualStyles;
using Library;

namespace WizWar1 {
interface ICard : ITarget, ICopiable<ICard> {
    ICardable WrappedCard { get; set; }
    
    String Name { get; set; }

    string Description { get; set; }

    String ToString();
}
}
