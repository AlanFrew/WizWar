using System;
using System.Collections.Generic;

namespace Library {
/*the Index class stores an array of Indexable objects. An Indexable object has an int[] that holds a set of indexes.
 * The index array has its most significant index first. The Index will be in ascending order after calling ReIndex().
 * The Index is not guaranteed sorted otherwise. */

/*Index is used in conjunction with SuperSearcher to find out which objects the mouse is over. Each objects stores
 * the minX, minY, maxX, and maxY of its box in its indexes field. The SuperSearch returns all objects whose
 * minX is less than the mouse coordinate, then refines that list based on the other three indexes.
 * 
 * Finding the objects under the mouse takes log(n) time. Keeping the list sorted takes nlog(n) time. The list neeeds
 * to be resorted every time something moves. The alternative is to check each object for collision with the mouse
 * on every mouse click, taking linear time. However, checking all objects for collisions with each other would 
 */

 /*Perhaps it would be best to use a simpler collision algorithm and alter Index to use single int indexes instead of
  * int arrays. That way it would be a basic array class, but with a binary search that can return ranges.
  */
    
public class Index<T> where T : IIndexable, new() {

    private class Comparer : IComparer<T> {
        public int Compare(T tFirst, T tSecond) {
            int ordersOfSignificance = tFirst.indexes.Length;
            for (int i = 0; i < ordersOfSignificance; ++i) {
                if (tFirst.indexes[i] != tSecond.indexes[i]) {
                    return tFirst.indexes[i] - tSecond.indexes[i];
                }
            }
            //completely equal
            return 0;
        }
    }

    private T[] data;

    private bool indexed;
    public bool Indexed {
        get {
            return indexed;
        }
    }

    private int capacity;
    public int Capacity {
        get {
            return capacity;
        }
        set {
            if (value != capacity) {
                capacity = value;
                T[] temp = new T[capacity];

                int t = Math.Min(capacity, count);
                for (int i = 0; i < t; ++i) {
                    temp[i] = data[i];
                }

                if (capacity < count) {
                    count = capacity;
                }
            }
        }
    }

    private int count;
    public int Count {
        get { return count; }
    }

    public Index() {
        capacity = 1;
        data = new T[capacity];
        indexed = true;

    }

    public Index(int tCapacity) {
        data = new T[tCapacity];
        indexed = false;
    }

    public void Add(T tItem, bool reIndex) {
        if (count == capacity) {
            Capacity *= 2;
        }

        data[count] = tItem;
        count++;

        if (reIndex == true) {
            ReIndex();
        }
    }

    public void Remove(T tItem) {
        for (int i = 0; i < data.Length; ++i) {
            if (data[i].Equals(tItem)) {
                for (int j = i; j < data.Length - 1; ++j) {
                    data[j] = data[j + 1];
                }
                data[data.Length - 1] = default(T);
            }
        }
    }

    public void ReIndex() {
        Array.Sort(data, new Comparer());

        indexed = true;
    }

    public T[] WhosUnderMyMouse(int mouseX, int mouseY) {
        return SuperSearcher<T>.SuperSearch(data, mouseX, mouseY);
    }

    //public T FindFirst(int tIndex) {
    //    if (indexed == false) {
    //        return default(T);
    //    }

    //    int index = IndexSearcher<T>.FindFirstIndex(data, tIndex);
    //    return data[index];
    //}
}
}
