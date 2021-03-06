﻿using System;
using System.Collections.Generic;

namespace WizWar1 {
    //this class should be able to set each spell's Name property based on the class type. Not activated
    static class CardExtension {
        private static Dictionary<Type, String> foo = new Dictionary<Type, String>();

        public static String GetName(this ICard me) {
            return foo[me.GetType()];
        }

        public static void SetName(this ICard me, String tName) {
            try {
                foo.Add(me.GetType(), tName);
            }
            catch (ArgumentException) {
            }
        }
    }
}
