using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Drawing;
using Library;
using System.Reflection;

namespace Library {

static class LibraryFunctions {
    public class ArraySearcher2D<T> where T : IComparable {
        public static int ArraySearch2D(T[,] tArray, T tTarget) {
            int index = tArray.GetLength(1) / 2;
            int lastIndex = index;
            int upperBound = tArray.Length - 1;
            int lowerBound = 0;

            while (true) {
                int temp = tArray[1, index].CompareTo(tTarget);
                //Console.WriteLine("Loop Start. Index = " + index + " lastIndex = " + lastIndex + " temp = " + temp);
                if (temp == 0) {
                    if (tArray[1, index - 1].CompareTo(tArray[1, index]) != 0) {
                        return index;
                    }
                    else {
                        //Console.WriteLine("Not the first " + tTarget + "! Previous item is: " + tArray[index - 1]);
                        lastIndex = index;
                        upperBound = index;
                        index = (lowerBound + upperBound) / 2;
                        continue;
                    }
                }

                if (Math.Abs(index - lastIndex) == 1) {
                    break;
                }

                if (temp < 0) {
                    lastIndex = index;
                    lowerBound = index;
                    index = (lowerBound + upperBound) / 2;
                }
                else {
                    lastIndex = index;
                    upperBound = index;
                    index = (lowerBound + upperBound) / 2;
                }
            }
            return -1;
        }
    }

    public class ArraySearcher<T> where T : IComparable{
        //finds arbitrary instance that compares equal to target
        public static int ArraySearch(T[] tArray, T tTarget) {
            int index = tArray.Length / 2;
            int lastIndex = index;
            int upperBound = tArray.Length - 1;
            int lowerBound = 0;
            while (true) {
                
                int temp = tArray[index].CompareTo(tTarget);
                //Console.WriteLine("Loop Start. Index = " + index + " lastIndex = " + lastIndex + " temp = " + temp);
                if (temp == 0) {
                    return index;
                }

                if (Math.Abs(index - lastIndex) == 1) {
                    break;
                }
                    
                if (temp > 0) {
                    //Console.WriteLine("Index decreased");
                    lastIndex = index;
                    upperBound = index;
                    index = (lowerBound + upperBound) / 2;
                }
                else if (temp < 0) {
                    //Console.WriteLine("Index increased.");
                    lastIndex = index;
                    lowerBound = index;
                    index = (lowerBound + upperBound) / 2;
                }
            }
            return -1;
        }
    }

    public class IndexSearcher<T> where T : IIndexable {
        //finds first instance that compares equal to target
        public static int FindFirstIndex(T[] tArray, int tIndex) {
            int index = tArray.Length / 2;
            int lastIndex = index;
            int upperBound = tArray.Length - 1;
            int lowerBound = 0;

            while (true) {
                int temp = tArray[index].indexes[0] - tIndex;
                //Console.WriteLine("Loop Start. Index = " + index + " lastIndex = " + lastIndex + " temp = " + temp);
                if (temp == 0) {
                    if (tArray[index - 1].indexes[0] != tIndex) {
                        return index;
                    }
                    else {
                        //Console.WriteLine("Not the first " + tTarget + "! Previous item is: " + tArray[index - 1]);
                        lastIndex = index;
                        upperBound = index;
                        index = (lowerBound + upperBound) / 2;
                        continue;
                    }
                }

                if (Math.Abs(index - lastIndex) == 1) {
                    break;
                }

                if (temp > 0) {
                    lastIndex = index;
                    lowerBound = index;
                    index = (lowerBound + upperBound) / 2;
                }
                else {
                    lastIndex = index;
                    upperBound = index;
                    index = (lowerBound + upperBound) / 2;
                }
            }
            return -1;
        }

        //finds last instance that compares equal to target
        public static int FindLastIndex(T[] tArray, int tIndex) {
            int index = tArray.Length / 2;
            int lastIndex = index;
            int upperBound = tArray.Length - 1;
            int lowerBound = 0;

            while (true) {
                int temp = tArray[index].indexes[0] - tIndex;
                //Console.WriteLine("Loop Start. Index = " + index + " lastIndex = " + lastIndex + " temp = " + temp);
                if (temp == 0) {
                    if (tArray[index + 1].indexes[0] != tIndex) {
                        return index;
                    }
                    else {
                        //Console.WriteLine("Not the first " + tTarget + "! Previous item is: " + tArray[index - 1]);
                        lastIndex = index;
                        lowerBound = index;
                        index = (lowerBound + upperBound) / 2;
                        continue;
                    }
                }

                if (Math.Abs(index - lastIndex) == 1) {
                    break;
                }

                if (temp > 0) {
                    lastIndex = index;
                    lowerBound = index;
                    index = (lowerBound + upperBound) / 2;
                }
                else {
                    lastIndex = index;
                    upperBound = index;
                    index = (lowerBound + upperBound) / 2;
                }
            }
            return -1;
        }

        //returns all instances that compare equal to target
        public T[] FindAllEqual(T[] tArray, int tIndex) {
            int lower = FindFirstIndex(tArray, tIndex);
            int upper = FindLastIndex(tArray, tIndex);

            T[] result = new T[upper - lower + 1];

            for (int i = lower; i < upper; ++i) {
                result[i - lower] = tArray[i];
            }
            return result;
        }
    }

    

    

    public class TypeMismatchException : Exception {
    }


    public static void Swap<T>(ref T first, ref T second) {
        if (first.GetType() != second.GetType()) {
            throw new TypeMismatchException();
        }

        T temp = first;
        first = second;
        second = temp;
    }

    public static void Swap(ref Object first, ref Object second) {
        if (first.GetType() != second.GetType()) {
            throw new TypeMismatchException();
        }

        Object temp = first;
        first = second;
        second = temp;
    }

    //Given an index to a wraparound collection (where the first and last node are connected), computes the index that is rawIndex nodes away from the first node, wrapping around as appropriate
    public static int IndexFixer(int rawIndex, int collectionSize) {
        return (((rawIndex % collectionSize) + collectionSize) % collectionSize);
    }

    public static double IndexFixer(double rawIndex, double collectionSize) {
        return (((rawIndex % collectionSize) + collectionSize) % collectionSize);
    }

    public static String ArrayToPaddedString<T, U>(T[] source, U pad) {
        StringBuilder stringbuilder = new StringBuilder();
        foreach (T t in source) {
            stringbuilder.Append(t.ToString());
            stringbuilder.Append(pad.ToString());
        }
        stringbuilder.Length -= 1;

        return stringbuilder.ToString();
    }

    public static List<T> RecursiveCopyList<T>(this List<T> tList) {

        List<T> result = new List<T>();
        foreach (T t in tList) {
            if (t is ICopiable<T>) {
                result.Add((t as ICopiable<T>).RecursiveCopy());
            }
            else {
                //throw new InvalidOperationException("Deep copying of this object is not available");
                
                //cheat to call MemberwiseClone() because it is protected
                System.Reflection.MethodInfo inst = t.GetType().GetMethod("MemberwiseClone",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (inst != null) {
                    result.Add((T)inst.Invoke(t, null));
                }
                else {
                    throw new NotSupportedException();
                }
            }
        }

        //I have no idea why this was here
        //throw new UnreachableException();
        return result;
    }

    public class Pair<T> : IComparable where T : IComparable {

        public T first;
        public T second;

        public Pair(T tFirst, T tSecond) {
            first = tFirst;
            second = tSecond;
        }

        public Pair() {
            //empty
        }

        public int CompareTo(Object tOther) {
            Pair<T> other = tOther as Pair<T>;
            if (this.first.CompareTo(other.first) == 0) {
                return (this.second.CompareTo(other.second));
            }

            return (this.first.CompareTo(other.first));
        }
    }

    public enum Case {
        None, First, Second, Both
    }

    public static Case FindCase(bool firstCondition, bool secondCondition) {
        if (firstCondition == true) {
            if (secondCondition == true) {
                return Case.Both;
            }
            else {
                return Case.First;
            }
        }
        else if (secondCondition == true) {
            return Case.Second;
        }
        else {
            return Case.None;
        }
    }

    public static double VectorToAngle(double tX, double tY) {
        if (tX == 0 && tY == 0) {
            return Double.NaN;
        }
        else {
            return Math.Atan2(tX, tY);
        }
    }
}
}
