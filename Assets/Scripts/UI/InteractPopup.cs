using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InteractPopup : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    private Player player;

    private void Start() {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.player = FindObjectOfType<Player>();
    }

    private void Update() {
        IInteractable interactable = player.GetInteractTarget();
        IDealable dealable = player.GetDealableTarget();

        if(interactable == null && dealable == null) {
            this.transform.position = new Vector3(-99, -99, 0);
        } else if(interactable == null) {
            this.spriteRenderer.sprite = sprites[1];
            this.transform.position = dealable.GetPosition();
        } else {
            this.spriteRenderer.sprite = sprites[0];
            this.transform.position = interactable.GetPosition();
        }
    }
}
