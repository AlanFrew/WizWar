using System.Collections.Generic;

namespace WizWar1 {
interface IAimable {
    object Aimable { get; }

    ITarget Target { get; set; }

    bool IsValidTarget(ITarget tTarget);

    bool IsValidTargetParent(ITarget tTarget);

    double ShotDirection { get; set; }

    List<TargetTypes> ValidTargetTypes { get; set; }

    bool IsValidTargetType(TargetTypes tTargetType);

    Wizard Controller { get; set; }
}
}
