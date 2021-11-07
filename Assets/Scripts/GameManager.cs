using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, ISubscriber {
    private static GameManager gameManager;

    private void Awake() {
        if(gameManager) Destroy(this);
        gameManager = this;
    }

    private void Start() {
        EventManager.Get()
            .Subscribe((StartEvent startEvent) => OnStart(startEvent));
    }

    private void OnStart(StartEvent startEvent) {
        // EventManager.Get().BroadcastWithDelay(new ClassStartEvent(new Vector2(0.0f, -0.85f)), 5.0f);
    }

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
