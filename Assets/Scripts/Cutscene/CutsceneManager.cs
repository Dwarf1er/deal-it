using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour {
    private Cutscene[] cutscenes;

    private void Start() {
        this.cutscenes = FindObjectsOfType<Cutscene>();
    }
}
