/// Modified from https://gist.github.com/irsal/5589011.

using System;
using UnityEngine;

public class BinaryHeap<T, K> where K: IComparable {
    private T[] data;
    private K[] priorities;
    private int count;
    private bool isMin;

    public BinaryHeap(bool isMin) {
        this.isMin = true;
        this.data = new T[1024];
        this.priorities = new K[1024];
        this.count = 0;
    }

    public int Count {
        get { return count; }
    }

    public void Push(T item, K priority) {
        int position = count++;
        data[position] = item;
        priorities[position] = priority;
        MoveUp(position);
    }

    public T Pop() {
        T poppedNode = data[0];
        Swap(0, --count);
        MoveDown(0);
        return poppedNode;
    }

    private bool ComparePriorities(int priorityIndex1, int priorityIndex2) {
        if(priorityIndex1 >= count || priorityIndex2 >= count) return false;

        int compareValue = priorities[priorityIndex1].CompareTo(priorities[priorityIndex2]);

        if(isMin) return compareValue <= 0;
        else return compareValue >= 0;
    }

    private void MoveUp(int position) {
        if(position <= 0) return;

        int parentPosition = Parent(position);
        if(ComparePriorities(parentPosition, position)) return;
        
        Swap(position, parentPosition);
        MoveUp(parentPosition);
    }

    private void MoveDown(int position) {
        int lchild = LeftChild(position);
        int rchild = RightChild(position);
        int largest = position;

        if(ComparePriorities(lchild, largest)) largest = lchild;
        if(ComparePriorities(rchild, largest)) largest = rchild;
        if(largest == position) return;

        Swap(position, largest);
        MoveDown(largest);
    }

    private void Swap(int position1, int position2) {
        T temp = data[position1];
        data[position1] = data[position2];
        data[position2] = temp;

        K temp2 = priorities[position1];
        priorities[position1] = priorities[position2];
        priorities[position2] = temp2;
    }

    private static int Parent(int position) {
        return (position - 1) / 2;
    }

    private static int LeftChild(int position) {
        return 2 * position + 1;
    }

    private static int RightChild(int position) {
        return 2 * position + 2;
    }
}
