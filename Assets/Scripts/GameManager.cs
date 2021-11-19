using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, ISubscriber {
    private static GameManager gameManager;

    private void Awake() {
        if(gameManager) Destroy(this);
        gameManager = this;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }

    private void Start() {}

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);    
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return transform;
    }
}
