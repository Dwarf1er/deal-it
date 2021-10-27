using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Graph<T> : MonoBehaviour where T: INode {
    protected List<T> nodes = new List<T>();
    private Queue<T[]> pathHistory = new Queue<T[]>();

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.black;

        foreach(T node in nodes) {
            Gizmos.DrawWireSphere(node.GetPosition(), 0.04f);

            foreach(T otherNode in node.GetNeighbors()) {
                Gizmos.DrawLine(node.GetPosition(), otherNode.GetPosition());
            }
        }

        
        foreach(T[] path in pathHistory) {
            for(int i = 0; i < path.Length; i++) {
                T node = path[i];
                float t = (float)i / (float)path.Length;

                Gizmos.color = Color.Lerp(Color.red, Color.blue, t);
                Gizmos.DrawSphere(node.GetPosition(), 0.04f);
            }
        }
    }

    public T RandomNode() {
        return nodes[Random.Range(0, nodes.Count)];
    }

    private void AddPathToHistory(T[] path) {
        if(path == null) return;
        if(pathHistory.Count > 5) pathHistory.Dequeue();
        pathHistory.Enqueue(path);
    }

    public T GetNearestNode(Vector3 target) {
        if(this.nodes.Count == 0) throw new System.Exception("Node nodes in graph.");

        T minNode = nodes[0];
        float minDistance = Vector3.Distance(minNode.GetPosition(), target);

        for(int i = 1; i < nodes.Count; i++) {
            T node = nodes[i];

            float distance = Vector3.Distance(node.GetPosition(), target);
            if(distance < minDistance) {
                minNode = node;
                minDistance = distance;
            }
        }

        return minNode;
    }

    /// Gets shortest path using BFS.
    public T[] GetPathTo(Vector3 from, Vector3 to) {
        T fromNode = GetNearestNode(from);
        T toNode = GetNearestNode(to);

        HashSet<T> seenNodes = new HashSet<T>();

        Queue<Stack<T>> queue = new Queue<Stack<T>>();
        queue.Enqueue(new Stack<T>(new T[]{fromNode}));

        T[] finalPath = new T[]{};
        while(queue.Count > 0) {
            Stack<T> nextPath = queue.Dequeue();
            T nextNode = nextPath.Pop();

            if(seenNodes.Contains(nextNode)) continue;

            if(nextNode.Equals(toNode)) {
                nextPath.Push(nextNode);
                finalPath = new Stack<T>(nextPath).ToArray();
                break;
            }

            nextPath.Push(nextNode);
            seenNodes.Add(nextNode);

            foreach(T neighbor in nextNode.GetNeighbors()) {
                Stack<T> neighborPath = new Stack<T>(new Stack<T>(nextPath));
                neighborPath.Push(neighbor);
                queue.Enqueue(neighborPath);
            }
        }

        AddPathToHistory(finalPath);
        
        return finalPath;
    }
}
