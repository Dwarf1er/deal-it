using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Glitch : MonoBehaviour {
    [SerializeField] private Tilemap[] tilemaps; 
    [SerializeField] private Transform environment;
    [SerializeField] private Transform entities;
    private SpriteRenderer[] renderers;
    private Color color = Color.white;
    private float rotationSpeed = 1.0f;
    private float flickerSpeed = 1.0f;
    private float t = 0;
    public static bool glitch = false;

    private void Start() {
        List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

        spriteRenderers.AddRange(environment.GetComponentsInChildren<SpriteRenderer>());
        spriteRenderers.AddRange(entities.GetComponentsInChildren<SpriteRenderer>());

        this.renderers = spriteRenderers.ToArray();
    }

    private void Update() {
        if(!glitch) return;

        t += Time.deltaTime;

        foreach(Tilemap tilemap in tilemaps) {
            tilemap.color = color;
        }

        foreach(SpriteRenderer renderer in renderers) {
            renderer.color = color;
        }

        Camera.main.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        rotationSpeed += 5.0f * Time.deltaTime;
        color = Color.HSVToRGB(0, 0, Mathf.Abs(Mathf.Cos(t * flickerSpeed)));
    }
}
