using System;

namespace WizWar1 {
interface IItem : IDestroyable, ILocatable, ICarriable, IAimable, ICardable {
    

    ITarget ItemTarget { get; set; }

    bool RequiresLoS { get; set; }

    Wizard Creator { get; set; }

    void OnGainParent(Wizard tHolder);

    void OnLossParent(Wizard tDropper);

    [Obsolete]
    void OnActivationChild();

    void OnActivationParent();

    void OnResolutionChild();

    void OnResolutionParent();

    IItemUsage UseItem();
}
}