using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    private static readonly string[] PLAYER_NAMES = new string[]{"P1", "P2"};
    private static InputManager inputManager;
    private Dictionary<string, bool> hadInput;

    void Start() {
        if(inputManager) Destroy(this);
        inputManager = this;

        hadInput = new Dictionary<string, bool>();
        foreach(string player in PLAYER_NAMES) {
            hadInput[player] = false;
        }
    }

    void Update() {
        /// TODO: Per player check.
        if(Input.GetKeyDown(KeyCode.Space)) {
            EventManager.Get().Broadcast(new DealInputEvent("P1"));
            EventManager.Get().Broadcast(new InteractInputEvent("P1"));
        }
        if(Input.GetKeyDown(KeyCode.E)) {
            EventManager.Get().Broadcast(new DialogueInputEvent("P1"));
        }
        if(Input.GetKeyDown(KeyCode.Q)) {
            EventManager.Get().Broadcast(new PanelInputEvent("P1"));
        }

        foreach(string player in PLAYER_NAMES) {
            Vector2 direction = new Vector2(Input.GetAxis(player + "Horizontal"), Input.GetAxis(player + "Vertical")).normalized;

            if(direction.magnitude != 0) {
                hadInput[player] = true;
                EventManager.Get().Broadcast(new MoveInputEvent(player, direction));
            } else if(hadInput[player]) {
                hadInput[player] = false;
                EventManager.Get().Broadcast(new MoveInputEvent(player, direction));
            }
        }
    }
}
