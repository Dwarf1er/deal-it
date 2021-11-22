using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float speed = 5.0f;
    public Transform[] targets;
    private Camera _camera;
    private float baseZoom = 0.4f;
    private float zoomExpandSpeed = 0.5f;

    void Start() {
        this._camera = this.GetComponent<Camera>();

        Vector3 targetPosition = this.AverageTargetPosition();
        targetPosition.z = this.transform.position.z;

        this.transform.position = targetPosition;
    }

    private Vector3 AverageTargetPosition() {
        Vector3 averagePosition = new Vector3();

        if(this.targets.Length == 0) return transform.position;

        foreach(Transform target in this.targets) {
            averagePosition += target.position;
        }

        averagePosition /= this.targets.Length;

        return averagePosition;
    }

    private float CameraZoom() {
        Vector3 averagePosition = this.AverageTargetPosition();
        float longestDistance = 0;

        foreach(Transform target in this.targets) {
            float distance = Vector3.Distance(target.position, averagePosition);

            if(distance > longestDistance) longestDistance = distance;
        }

        return longestDistance * this.zoomExpandSpeed + this.baseZoom;
    }
    
    void Update() {
        Vector3 targetPosition = this.AverageTargetPosition();
        targetPosition.z = this.transform.position.z;

        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * this.speed);
        this._camera.orthographicSize = this.CameraZoom();
    }
}
