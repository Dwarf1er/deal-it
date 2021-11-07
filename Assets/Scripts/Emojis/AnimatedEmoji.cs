using System.Collections;
using UnityEngine;

public class AnimatedEmoji : MonoBehaviour {
    public float duration = 1.0f;
    public string path;
    private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        sprites = Resources.LoadAll<Sprite>(path);
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(Animate());
    }

    private IEnumerator Animate() {
        for(int i = 0; i < sprites.Length; i++) {
            spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(duration / sprites.Length);
        }
    }
}