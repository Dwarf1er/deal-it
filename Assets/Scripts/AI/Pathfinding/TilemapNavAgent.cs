using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapNavAgent : MonoBehaviour {
    private TilemapNavMesh navMesh;

    private void Start() {
        navMesh = FindObjectOfType<TilemapNavMesh>();
    }

    public Vector2[] GetPath(Vector2 from, Vector2 to) {
        return navMesh.GetPath(from, to);
    }

    public Vector2 RandomPosition() {
        return navMesh.RandomPosition();
    }
}
