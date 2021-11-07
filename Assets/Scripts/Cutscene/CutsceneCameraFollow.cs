using System.Collections;
using UnityEngine;

public class CutsceneCameraFollow : CutsceneAbstract {
    public Transform[] targets;

    public override void Enter() {
        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.targets = targets;
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {

    }
}
