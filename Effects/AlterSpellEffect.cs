using System.Reflection;

namespace WizWar1 {
class AlterSpellEffect : Effect {
    public PropertyInfo TargetMember;
    public object NewValue;

    public AlterSpellEffect(PropertyInfo tInfo, object tValue) {
        TargetMember = tInfo;
        NewValue = tValue;
    }

    public override void OnRunChild() {
        TargetMember.SetValue(target, NewValue, null);
    }
}
}