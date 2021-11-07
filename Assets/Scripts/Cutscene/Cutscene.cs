using UnityEngine;

public class Cutscene : MonoBehaviour {
    private CutsceneAbstract[] steps;
    private int currentStep = 0;
    private bool stepStarted = false;

    private void Start() {
        steps = transform.GetComponentsInChildren<CutsceneAbstract>();
    }

    private void Update() {
        if(currentStep >= steps.Length) return;

        if(!stepStarted) {
            stepStarted = true;
            steps[currentStep].Enter();
        }

        if(!steps[currentStep].Loop()) {
            stepStarted = false;
            steps[currentStep++].Exit();
        }
    }
}