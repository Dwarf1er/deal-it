using System;
using System.Collections.Generic;

public class PriorityQueue<T, K> where K : IComparable {
    private BinaryHeap<T, K> heap;

    public PriorityQueue(bool isMin) {
        this.heap = new BinaryHeap<T, K>(isMin);
    }

    public int Count {
        get { return heap.Count; }
    }

    public void Enqueue(T value, K priority) {
        heap.Push(value, priority);
    }

    public T Dequeue() {
        return heap.Pop();
    }
}
