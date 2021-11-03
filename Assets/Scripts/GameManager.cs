using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    RequireComponent(typeof(EventManager)), 
    RequireComponent(typeof(SoundManager)), 
    RequireComponent(typeof(InputManager)), 
    RequireComponent(typeof(UIManager)),
    RequireComponent(typeof(EmojiManager))
]
public class GameManager : MonoBehaviour {
    private static GameManager gameManager;

    private void Awake() {
        if(gameManager) Destroy(this);
        gameManager = this;
    }

    private void Start() {
        EventManager.Get().Broadcast(new StartEvent());
        EventManager.Get().BroadcastWithDelay(new ClassStartEvent(new Vector2(0.0f, -0.85f)), 5.0f);
    }

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);    
    }
}
