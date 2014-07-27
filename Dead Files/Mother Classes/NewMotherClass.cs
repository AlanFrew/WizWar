using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
abstract partial class NewMotherClass : IChangeling {

    protected bool validWall;
    public bool ValidWall {
        get {
            return validWall;
        }
        set {
            if (value == true) {
                validTarget = true;
            }
            
            validWall = value;
            
        }
    }

    protected bool validCard;
    public bool ValidCard {
        get {
            return validCard;
        }
        set {
            validCard = value;
            validSpell = value;
        }
    }

    protected bool validSpell;
    public bool ValidSpell {
        get {
            return validSpell;
        }
        set {
            validSpell = value;
            ValidCard = value;
            validCastingTypes = new List<SpellType>();
            validTargets = new List<TargetTypes>();
        }
    }

    protected bool validCreation;
    public bool ValidCreation {
        get {
            return validCreation;
        }
        set {
            if (value == true) {
                ValidSpell = true;
                ValidCard = true;
                validCastingTypes.Add(SpellType.Neutral);
            }

            validCreation = value;
        }
    }

    protected bool validItem;
    public bool ValidItem {
        get {
            return validItem;
        }
        set {
            if (value == true) {
                validSpell = true;
                validCard = true;
                validCastingTypes.Add(SpellType.Item);
                validTargets.Add(TargetTypes.None);
                itemTargetTypes = new List<TargetTypes>();
            }

            validItem = true;
        }
    }

    protected bool validTarget;
    public bool ValidTarget {
        get {
            return validTarget;
        }
        set {
            validTarget = value;
        }
    }

    protected bool validLocatable;
    public bool ValidLocatable {
        get {
            return validLocatable;
        }
        set {
            validLocatable = value;
        }
    }

    protected bool isWall;
    public bool IsWall {
        get {
            return isWall;
        }
    }

    protected bool isCard;
    public bool IsCard {
        get {
            return isCard;
        }
    }

    protected bool isSpell;
    public bool IsSpell {
        get {
            return isSpell;
        }
    }

    protected bool isCreation;
    public bool IsCreation {
        get {
            return isCreation;
        }
    }

    protected bool isItem;
    public bool IsItem {
        get {
            return isItem;
        }
    }

    protected bool isTarget;
    public bool IsTarget {
        get {
            return isTarget;
        }
    }

    protected bool isLocatable;
    public bool IsLocatable {
        get {
            return isLocatable;
        }
    }

    public NewMotherClass() {
        isWall = false;
        isCard = false;
        isSpell = false;
        isCreation = false;
        isItem = false;
        isTarget = false;
        isLocatable = false;
    }

#region IChangeling

    public void BecomeCard() {
        throw new NotImplementedException();
    }

    public void BecomeWall() {
        throw new NotImplementedException();
    }

    public void BecomeCreation() {
        if (validCreation == false || X < 0 || Y < 0 || Square == null) {
            throw new InvalidTypeException();
        }

        isCreation = true;
    }

    public void BecomeItem() {
        throw new NotImplementedException();
    }

    public void BecomeLocatable() {
        if (x < 0 || y < 0 || square == null) {
            return;
        }

        isLocatable = true;
    }

    public void BecomeTarget() {
        if (activeTargetType == TargetTypes.Undef) {
            return;
        }

        isTarget = true;
    }

    public void BecomeTarget(TargetTypes tActiveTargetType) {
        if (validTarget == false) {
            throw new InvalidTypeException();
        }

        activeTargetType = tActiveTargetType;
        isTarget = true;
    }

    public void BecomeSpell() {
        if (caster == null || activeTargetType == TargetTypes.Undef || target == null) {
            throw new NotReadyException();
        }

        isSpell = true;
    }

    public void BecomeSpell(Wizard tCaster, ITarget tTarget, SpellType tActiveSpellType) {
        if (validSpell == false) {
            return;
        }

        caster = tCaster;
        target = tTarget;
        activeSpellType = tActiveSpellType;
        isSpell = true;
    }

    #endregion
}
}