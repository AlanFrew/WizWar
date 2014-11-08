using System.Collections.Generic;
using Library;

namespace WizWar1 {
interface ISpell : ITarget, ISpellOrItemUsage, ICopiable<Spell>, IStackable, IAimable, ICardable {
    string Name { get; set; }

    Wizard Caster { get; set; }

    ITarget SpellTarget { get; set; }

    SpellType ActiveSpellType { get; }

    bool RequiresLoS { get; set; }

    List<SpellType> ValidCastingTypes { get; }

    bool IsValidCastingType(SpellType tSpellType);

    bool IsOnlyValidCastingType(SpellType tSpellType);

    void OnResolution();

    void OnCast();

    List<Marker> Markers { get; set; }
}
}
