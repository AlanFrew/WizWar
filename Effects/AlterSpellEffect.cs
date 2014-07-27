using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WizWar1 {
class AlterSpellEffect : Effect {
    public PropertyInfo targetMember;
    public object newValue;

    public AlterSpellEffect(PropertyInfo tInfo, object tValue) {
        targetMember = tInfo;
        newValue = tValue;
    }

    public override void OnRunChild() {
        targetMember.SetValue(target, newValue, null);
    }
}
}
