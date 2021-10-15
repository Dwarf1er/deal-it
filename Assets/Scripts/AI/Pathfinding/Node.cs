using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    public Node[] neighbors;

    public Node RandomNeighbor() {
        return this.neighbors[Random.Range(0, this.neighbors.Length)];
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.1f);

        foreach(Node neighbor in this.neighbors) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, neighbor.transform.position);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(neighbor.transform.position, 0.1f);
        }
    }
}
