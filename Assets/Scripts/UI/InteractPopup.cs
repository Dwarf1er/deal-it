using UnityEngine;

public class InteractPopup : MonoBehaviour {
    private Player player;

    private void Start() {
        this.player = FindObjectOfType<Player>();
    }

    private void Update() {
        IInteractable interactable = player.GetInteractTarget();

        if(interactable == null) {
            this.transform.position = new Vector3(-99, -99, 0);
        } else {
            this.transform.position = interactable.GetPosition();
        }
    }
}
