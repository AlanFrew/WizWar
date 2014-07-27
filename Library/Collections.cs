using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Library {
class RestrictedList<T> : List<T> {
    public new void Add(T tItem) {
        throw new NotSupportedException();
    }

    public void PleaseAdd(T tItem) {
        base.Add(tItem);
    }
}

//avoids "collection modified" errors
class RobustList<T> : IEnumerable<T> {
    private class Node {
        public Node previous;
        public Node next;
        public T data;
    }

    Node firstNode;
    Node lastNode;

    private class RobustListEnumerator : IEnumerator<T> {
        private RobustList<T> myList;
        private Node current;

        public RobustListEnumerator(RobustList<T> tList) {
            myList = tList;
        }

        public void Reset() {
            current = null;
        }

        public T Current {
            get {
                return current.data;
            }
        }

        object IEnumerator.Current {
            get {
                return current.data;
            }
        }

        public bool MoveNext() {
            if (myList.firstNode == null) {
                return false;
            }

            if (current == null) {
                current = myList.firstNode;
                return true;
            }

            if (current.next != null) {
                current = current.next;
                return true;
            }
            else {
                return false;
            }
        }

        void IDisposable.Dispose() {
            //empty
        }
    }

    public IEnumerator<T> GetEnumerator() {
        return new RobustListEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return new RobustListEnumerator(this);
    }

    //new Node gets added at the end so that Nodes added during a search get processed as well
    public void Add(T tData) {
        Node newNode = new Node();
        newNode.data = tData;

        newNode.previous = lastNode;
        newNode.next = null;

        if (firstNode == null) {
            firstNode = newNode;
        }
        else {
            lastNode.next = newNode;
        }

        lastNode = newNode;
    }

    public bool Remove(T tData) {
        if (firstNode == null) {
            return false;
        }

        Node testNode = firstNode;
        while (testNode != null) {
            if (testNode.data.Equals(tData)) {
                if (testNode != firstNode) {
                    testNode.previous.next = testNode.next;
                }
                else {
                    firstNode = firstNode.next;
                }

                if (testNode != lastNode) {
                    testNode.next.previous = testNode.previous;
                }
                else {
                    lastNode = testNode.previous;
                }
                //ShowOnScreen();
                return true;
            }

            testNode = testNode.next;
        }

        //ShowOnScreen();
        return false;
    }

    public void ShowOnScreen() {
        Node testNode = firstNode;
        while (testNode != null) {
            MessageBox.Show(testNode.data.ToString());
            testNode = testNode.next;
        }
    }
}
}