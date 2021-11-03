using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Graph<T> : MonoBehaviour where T: INode {
    protected List<T> nodes = new List<T>();
    private Queue<T[]> pathHistory = new Queue<T[]>();

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.black;

        foreach(T node in nodes) {
            Gizmos.color = Color.Lerp(Color.red, Color.blue, node.GetNeighbors().Count / 4.0f);
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

    /// Gets shortest path using A*.
    public T[] GetPathTo(Vector3 from, Vector3 to) {
        T fromNode = GetNearestNode(from);
        T toNode = GetNearestNode(to);

        PriorityQueue<Tuple<Stack<T>, HashSet<T>>, float> queue = new PriorityQueue<Tuple<Stack<T>, HashSet<T>>, float>();
        queue.Enqueue(new Tuple<Stack<T>, HashSet<T>>(new Stack<T>(new T[]{fromNode}), new HashSet<T>()), 0);

        T[] finalPath = new T[]{};
        while(queue.Count() > 0) {
            Tuple<Stack<T>, HashSet<T>> pathSeen = queue.Dequeue();
            Stack<T> path = pathSeen.first;
            HashSet<T> seen = pathSeen.second;
            T nextNode = path.Pop();

            if(seen.Contains(nextNode)) continue;

            if(nextNode.Equals(toNode)) {
                path.Push(nextNode);
                finalPath = new Stack<T>(path).ToArray();
                break;
            }

            path.Push(nextNode);
            seen.Add(nextNode);

            foreach(T neighbor in nextNode.GetNeighbors()) {
                Stack<T> neighborPath = new Stack<T>(new Stack<T>(path));
                neighborPath.Push(neighbor);
                queue.Enqueue(new Tuple<Stack<T>, HashSet<T>>(neighborPath, seen), 1.0f / (Vector2.Distance(neighbor.GetPosition(), to)));
            }
        }

        if(finalPath.Length == 0) {
            Debug.LogWarning("Could not find path.");
        }

        AddPathToHistory(finalPath);
        
        return finalPath;
    }
}
