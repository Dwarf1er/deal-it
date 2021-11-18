using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour {
    private HashSet<Cutscene> activeCutscenes;

    private void Start() {
        this.activeCutscenes = new HashSet<Cutscene>();

        foreach(CutsceneFile cutsceneFile in FindObjectsOfType<CutsceneFile>()) {
            foreach(Cutscene cutscene in cutsceneFile.GetCutscenes()) {
                cutscene.Enter();
                activeCutscenes.Add(cutscene);
            }
        }
    }

    private void Update() {
        HashSet<Cutscene> cutscenesToRemove = new HashSet<Cutscene>();

        foreach(Cutscene cutscene in activeCutscenes) {
            if(!cutscene.Loop()) cutscenesToRemove.Add(cutscene);
        }

        foreach(Cutscene cutscene in cutscenesToRemove) {
            cutscene.Exit();
            activeCutscenes.Remove(cutscene);
        }
    }
}
