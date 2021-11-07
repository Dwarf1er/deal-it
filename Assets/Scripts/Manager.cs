using UnityEngine;

[
    RequireComponent(typeof(EventManager)), 
    RequireComponent(typeof(SoundManager)), 
    RequireComponent(typeof(InputManager)), 
    RequireComponent(typeof(UIManager)),
    RequireComponent(typeof(EmojiManager)),
    RequireComponent(typeof(CutsceneManager)),
    RequireComponent(typeof(QuestManager)),
    RequireComponent(typeof(GameManager))
]
public class Manager : MonoBehaviour {
    private static Manager manager;

    private void Awake() {
        if(manager) Destroy(this);
        manager = this;
    }

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);    
    }
}
