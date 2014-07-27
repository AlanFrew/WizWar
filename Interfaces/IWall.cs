using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WizWar1 {
interface IWall : ILocatable {
    Image MyImage {
        get;
        set;
    }

    Square FirstNeighbor {
        get;
        set;
    }

    Square SecondNeighbor {
        get;
        set;
    }

    void ArrangeNeighbors();

    bool IsPassable(Wizard tWizard);

    bool IsVertical();

    bool IsHorizontal();
}
}
