using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour {
    public CutsceneAbstract[] steps;
    public int currentStep = 0;
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
