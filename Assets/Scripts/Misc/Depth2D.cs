using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depth2D : MonoBehaviour {
    void Update() {
        Vector3 position = this.transform.position;
        position.z = position.y;

        this.transform.position = position;
    }
}
