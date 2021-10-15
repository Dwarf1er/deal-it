using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {
    private Node[] nodes;

    void Start() {
        this.nodes = this.GetComponentsInChildren<Node>();
    }

    public Node GetNearestNode(Vector3 target) {
        Node minNode = null;
        float minDistance = float.MaxValue;

        foreach(Node node in this.nodes) {
            float distance = Vector3.Distance(node.transform.position, target);
            if(distance < minDistance) {
                minNode = node;
                minDistance = distance;
            }
        }

        return minNode;
    }

    /// Gets shortest path using BFS.
    public Node[] GetPathTo(Vector3 from, Vector3 to) {
        Node fromNode = this.GetNearestNode(from);
        Node toNode = this.GetNearestNode(to);

        HashSet<Node> seenNodes = new HashSet<Node>();

        Queue<Stack<Node>> queue = new Queue<Stack<Node>>();
        queue.Enqueue(new Stack<Node>(new Node[]{fromNode}));

        while(queue.Count > 0) {
            Stack<Node> nextPath = queue.Dequeue();
            Node nextNode = nextPath.Pop();

            if(seenNodes.Contains(nextNode)) continue;

            if(nextNode == toNode) {
                nextPath.Push(nextNode);
                return new Stack<Node>(nextPath).ToArray();
            }

            nextPath.Push(nextNode);
            seenNodes.Add(nextNode);

            foreach(Node neighbor in nextNode.neighbors) {
                Stack<Node> neighborPath = new Stack<Node>(new Stack<Node>(nextPath));
                neighborPath.Push(neighbor);
                queue.Enqueue(neighborPath);
            }
        }

        return new Node[]{};
    }
}
