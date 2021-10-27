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

    void Awake() {
        if(gameManager) Destroy(this);
        gameManager = this;
    }

    void Start() {
        Vector3 classPosition = new Vector2(0, -0.85f);

        EventManager.Get().BroadcastWithDelay(new ClassStartEvent(classPosition), 5.0f);
    }
}
