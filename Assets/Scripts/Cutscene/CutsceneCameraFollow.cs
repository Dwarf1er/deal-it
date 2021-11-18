using System.Collections.Generic;
using UnityEngine;

public class CutsceneCameraFollow : CutsceneAbstract {
    private Transform[] transformTargets;
    private CameraController cameraController;

    public CutsceneCameraFollow(string[] targets) {
        List<Transform> transformList = new List<Transform>();

        foreach(string target in targets) {
            transformList.Add(GameObject.Find(target).transform);
        }

        this.transformTargets = transformList.ToArray();

        this.cameraController = Camera.main.GetComponent<CameraController>();
    }

    public override void Enter() {
        cameraController.targets = transformTargets;
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
