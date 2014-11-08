using System;
using System.Collections.Generic;
using Library;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace WizWar1 {
public abstract class Effect : ITarget, ICopiable<Effect>, IStackable {
    internal ITarget target;
    internal Wizard Caster;
    internal ITarget source;
    internal List<Marker> markers;
    internal bool requiresLoS;
    internal List<Event> myEvents;

    public Effect() {
        requiresLoS = false;
        markers = new List<Marker>();
        myEvents = new List<Event>();
        //SetEvent();
    }

    //Effects should not create other effects
    public void RanChild(Effect tEffect) {
        throw new NotSupportedException();
    }

    //public virtual T RecursiveCopy2<T>(this T sourceEffect) where T : Effect {
    //    //return (System.Activator.CreateInstance(this.GetType()) as Effect);
    //    Type myType = this.GetType();
    //    StringBuilder sb = new StringBuilder();

    //    foreach (FieldInfo fi in myType.GetFields()) {
    //        sb.AppendLine(fi.ToString());
    //    }
    //    MessageBox.Show(sb.ToString());

    //    Effect result = Effect.New<T>(sourceEffect.Caster, sourceEffect.source, sourceEffect.target);
    //    foreach (FieldInfo fi in myType.GetFields()) {
    //        fi.SetValue(result, fi.GetValue);
    //    }
    //}

    public virtual Effect RecursiveCopy<T>(T source) where T : Effect {
        if (!typeof(T).IsSerializable) {
            throw new ArgumentException("The type " + source + " is not serializable");
        }

        if (source == null) {
            return default(T);
        }

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new MemoryStream();
        using (stream) {
            formatter.Serialize(stream, source);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }
    }

    public virtual Effect RecursiveCopy() {
        object temp = MemberwiseClone();
        Effect result = (Effect)temp;
        result.markers = markers.RecursiveCopyList();
        result.myEvents = myEvents.RecursiveCopyList();
        return result;
    }

    public void OnRun() {
        //if (myEvent != null) {
            //Type t = myEvent.GetType();
            //MethodInfo mi = typeof(Effect).GetMethod("Cast");
            //MethodInfo castMethod = typeof(Effect).GetMethod("Cast").MakeGenericMethod(t);
            //Event castedObject = (Event)castMethod.Invoke(null, new object[] { myEvent });
            //MethodInfo mi = EventSourceRewrite<

            //GameState.eventDispatcher.Notify(myEvent);

            //if () {
            //    duration--;
        OnRunChild();
        source.RanChild(this);
            //} 
    }

    public static T Cast<T>(T o) where T : Event {
        return o;
    }

    public abstract void OnRunChild();

    internal static T New<T>(Wizard tCaster, ITarget tSource, ITarget tTarget, int tDuration = 0) where T : Effect, new() {
        return Initialize<T>(tCaster, tSource, tTarget, new T(), tDuration);
    }

    internal static T Initialize<T>(Wizard tCaster, ITarget tSource, ITarget tTarget, T tEffect, int tDuration = 0) where T : Effect {
        tEffect.Caster = tCaster;
        tEffect.source = tSource;
        tEffect.target = tTarget;

        foreach (Marker m in tEffect.markers) {
            if (m is DurationBasedMarker) {
                var dbm = m as DurationBasedMarker;
                if (dbm.DurationBasedValue == 0.0) {

                    if (tSource is ISpell && tSource is INumberable) {
                        dbm.DurationBasedValue = (tSource as NumberedSpell).CardValue;
                        (m as DurationBasedMarker).DurationBasedValue = (tSource as NumberedSpell).CardValue;
                    }
                    else {
                        dbm.DurationBasedValue = 1;
                    }
                }
            }
        }

        return tEffect;
    }

    public TargetTypes ActiveTargetType {
        get {
            return TargetTypes.Effect;
        }
    }

    public bool IsTargetableAs(TargetTypes tTargetType) {
        if (tTargetType == TargetTypes.Effect) {
            return true;
        }

        return false;
    }

    public void BecomeTarget(TargetTypes tActiveSpellType) {
        //empty
    }

    //protected void SetEvent() {
    //    if (this is DamageEffect) {
    //        myEvent = new DamageEvent(Caster, source, target, (this as DamageEffect).Amount, (this as DamageEffect).damageType);
    //    }
    //}

    public override string ToString() {
        if (Caster != null) {
            return GetType().Name + " cast by " + Caster.Name;
        }

        return GetType().Name;
    }

    //[source]
    public class foo {
    }
    //[/source]

    //[destination]

    //[/destination]
}
}
