using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TilemapNavObstacle : MonoBehaviour {
    private BoxCollider2D boxCollider;
    private TilemapNavMesh navMesh;

    private void Start() {
        this.boxCollider = GetComponent<BoxCollider2D>();
        this.navMesh = FindObjectOfType<TilemapNavMesh>();
        navMesh.AddObstacle(this);
    }

    private void OnDestroy() {
        navMesh.RemoveObstacle(this);
    }

    public Bounds GetBounds() {
        return boxCollider.bounds;
    }
}
