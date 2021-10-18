using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EventManager)), RequireComponent(typeof(SoundManager)), RequireComponent(typeof(InputManager)), RequireComponent(typeof(UIManager))]
public class GameManager : MonoBehaviour {
    private static GameManager gameManager;

    void Awake() {
        if(gameManager) Destroy(this);
        gameManager = this;
    }

    void Start() {
        EventManager.Get().BroadcastDelayed(new ClassStartEvent(new Vector2(0, -0.85f)), 5.0f);
        EventManager.Get().BroadcastDelayed(new ClassEndEvent(), 30.0f);    
    }
}
