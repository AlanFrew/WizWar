using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;

namespace WizWar1 {
interface ISpell : ICard, ICopiable<Spell>, IStackable {
    Wizard Caster {
        get;
        set;
    }

    ITarget SpellTarget {
        get;
        set;
    }

    SpellType ActiveSpellType {
        get;
    }

    //void SetActiveSpellType(TargetTypes tTargetType);

    bool RequiresLoS {
        get;
        set;
    }

    List<Effect> EffectsWaiting {
        get;
        set;
    }

    //static ISpell MakeSpell(Wizard tCaster, ITarget tTarget, String tName);

    bool IsValidCastingType(SpellType tSpellType);

    bool IsValidSpellTargetParent(ITarget tTarget, Wizard tCaster);

    bool IsValidSpellTarget(ITarget tTarget, Wizard tCaster);

    bool IsValidSpellTargetType(TargetTypes tTargetType);

    void OnResolution();

    //void BecomeSpell();

    void OnCast();

    bool AcceptsNumber {
        get;
    }

    int CardValue {
        get;
        set;
    }

    double ShotDirection {
        get;
        set;
    }

    List<Marker> Markers  {
        get;
        set;
    }
}
}
