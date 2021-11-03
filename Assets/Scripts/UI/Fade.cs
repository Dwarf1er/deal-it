using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {
    public bool showOnLoad = false;
    public bool autoFadeIn = false;
    private Image image;
    private bool fadeOut = false;
    private float fadeSpeed = 1.0f;
    private bool transition = false;

    private void Start() {
        this.image = GetComponent<Image>();
        SetAlpha(showOnLoad ? 1.0f : 0.0f);
        if(autoFadeIn) FadeIn();
    }

    public bool IsTransitioning() {
        return transition;
    }

    private float GetAlpha() {
        return image.color.a;
    }

    private void SetAlpha(float alpha) {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    private void Update() {
        if(!transition) return;

        float target = fadeOut ? 1.0f : 0.0f;
        if(Mathf.Abs(GetAlpha() - target) < 0.001f) {
            SetAlpha(target);
            transition = false;
            return;
        }
        float direction = GetAlpha() - target > 0 ? -1 : 1;

        SetAlpha(GetAlpha() + direction * fadeSpeed * Time.deltaTime);
    }

    public void FadeIn() {
        SetAlpha(1.0f);
        transition = true;
        fadeOut = false;
    }

    public void FadeOut() {
        SetAlpha(0.0f);
        transition = true;
        fadeOut = true;
    }
}
