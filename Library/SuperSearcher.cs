using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;

namespace Library {
//supports searching using multiple dimensions
public class SuperSearcher<T> where T : IIndexable {
    //finds first instance that compares equal to target in the given range
    public static int FindFirstIndexBounded(T[] tArray, int tIndex, int tLower, int tUpper) {
        int upperBound = tLower;
        int lowerBound = tUpper;
        int index = (lowerBound + upperBound) / 2;
        int lastIndex = index;

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

    //finds last instance that compares equal to target in the given range
    public static int FindLastIndexBounded(T[] tArray, int tIndex, int tLower, int tUpper) {
        int upperBound = tUpper;
        int lowerBound = tLower;
        int index = (lowerBound + upperBound) / 2;
        int lastIndex = index;

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

    //first first instance that is no less than the index in the given range, comparing based on the given bit
    public static int FindAllNoLessThanBounded(T[] tArray, int tIndex, int tLower, int tUpper, int significance) {
        int lowerBound = tLower;
        int upperBound = tUpper;
        int index;
        //int lastIndex = index;

        while (true) {
            index = (lowerBound + upperBound) / 2;

            if (tArray[index].indexes[significance] >= tIndex) {
                //qualifies
                if (index == lowerBound || tArray[index - 1].indexes[significance] < tIndex) {
                    //index points to least qualifying object
                    break;
                }
                else {
                    upperBound = index;
                }
            }
            else {
                //does not qualify; search higher
                if (lowerBound == tUpper) {
                    //no qualifying objects
                    return -1;
                }
                //still room to search
                lowerBound = index;
            }
        }
        return index;
    }

    public static int FindAllNoGreaterThanBounded(T[] tArray, int tIndex, int tLower, int tUpper, int significance) {
        int lowerBound = tLower;
        int upperBound = tUpper;
        int index;
        //int lastIndex = index;

        while (true) {
            index = (lowerBound + upperBound) / 2;

            if (tArray[index].indexes[significance] <= tIndex) {
                //qualifies
                if (index == upperBound || tArray[index + 1].indexes[significance] > tIndex) {
                    //index points to greatest qualifying object
                    break;
                }
                else {
                    lowerBound = index;
                }
            }
            else {
                //does not qualify; search higher
                if (upperBound == 0) {
                    //no qualifying objects
                    return -1;
                }
                //still room to search
                upperBound = index;
            }
        }
        return index;
    }

    public static T[] SuperSearch(T[] tArray, int tX, int tY) {
        //find all objects that extend past the left of the x coordinate
        int minX = FindAllNoGreaterThanBounded(tArray, tX, 0, tArray.Length, 0);
        if (minX == -1) {
            return null;
        }
        //search that list for objects that extend past the right of the x coordinate
        int maxX = FindAllNoLessThanBounded(tArray, tX, minX, tArray.Length, 1);
        if (maxX == -1) {
            return null;
        }

        //search that list for objects that extend above the y coordinate
        int minY = FindAllNoGreaterThanBounded(tArray, tY, minX, maxX, 2);
        if (minY == -1) {
            return null;
        }

        //search that list for objects that extend below the y coordinate
        int maxY = FindAllNoLessThanBounded(tArray, tY, minY, maxX, 3);
        if (maxY == -1) {
            return null;
        }
        //the range [minY, maxY] now contains all objects that contain the mouse

        T[] result = new T[maxY - minY + 1];
        for (int i = minY; i < maxY; ++i) {
            result[i - minY] = tArray[i];
        }
        return result;
    }
}
}
