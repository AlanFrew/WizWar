//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace WizWar1 {
//class CreatedItem : Creation, ICreatedItem {
//    private Item myItem;

//    protected String itemName;
//    public String ItemName {
//        get {
//            return itemName;
//        }
//    }

//    protected Wizard carrier;
//    public Wizard Carrier {
//        get {
//            return carrier;
//        }
//        set {
//            carrier = value;
//        }
//    }

//    protected ITarget itemTarget;
//    public ITarget ItemTarget {
//        get {
//            return itemTarget;
//        }
//        set {
//            itemTarget = value;
//        }
//    }

//    protected List<TargetTypes> itemTargetTypes;

//    public CreatedItem() {
//        myItem = new Item(); //testing for use with RequiresLoS()
//        itemName = Name;
//        itemTargetTypes = new List<TargetTypes>();
//        //ValidCastingTypes.Add(SpellType.Item);
//        //validTargets.Add(TargetType.None);
//    }

//    public bool RequiresLoS {
//        get {
//            return myItem.RequiresLoS;
//        }
//        set {
//            value = myItem.RequiresLoS;
//        }
//    }

//    public bool IsValidTargetTypeForItem(TargetTypes tTargetType) {
//        foreach (TargetTypes t in itemTargetTypes) {
//            if (tTargetType == t) {
//                return true;
//            }
//        }

//        return false;
//    }

//    public bool IsValidTargetForItem(ITarget tTarget) {
//        foreach (TargetTypes t in itemTargetTypes) {
//            if (tTarget.ActiveTargetType == t) {
//                return true;
//            }
//        }

//        return false;
//    }

//    public virtual void OnActivationChild() {
//        throw new InvalidTypeException();
//    }

//    public void OnGainParent(Wizard tHolder) {
//        //empty by default
//    }

//    public void OnLossParent(Wizard tDropper) {
//        //empty by default
//    }

//    public void OnPlayChild() {
//        throw new NotImplementedException();
//    }

//    public void OnPlayParent() {
//        throw new NotImplementedException();
//    }
//}
//}
