using System;
using System.Collections.Generic;

public class PriorityQueue<T, K> where K : IComparable {
    private List<Tuple<T, K>> queue;

    public PriorityQueue() {
        queue = new List<Tuple<T, K>>();
    }

    public int Count() {
        return queue.Count;
    }

    public void Enqueue(T value, K priority) {
        queue.Add(new Tuple<T, K>(value, priority));
    }

    public T Dequeue() {
        if(queue.Count == 0) {
            throw new IndexOutOfRangeException("Trying to dequeue from an empty list.");
        }

        Tuple<T, K> maxPriority = queue[0];
        
        foreach(Tuple<T, K> tuple in queue) {
            if(tuple.second.CompareTo(maxPriority.second) >= 0) {
                maxPriority = tuple;
            }
        }

        queue.Remove(maxPriority);

        return maxPriority.first;
    }
}