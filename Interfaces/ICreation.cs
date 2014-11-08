using System.Drawing;

namespace WizWar1 {
interface ICreation : ILocatable, IDestroyable {
    Wizard Creator {
        get;
    }

    Image MyImage { get; set; }
}
}
