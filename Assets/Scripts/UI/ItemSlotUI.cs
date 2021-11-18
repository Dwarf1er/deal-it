using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour {
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private Image itemRenderer;
    [SerializeField] private Transform panel;
    [SerializeField] private Text itemCountText;
    private int itemCount;
    public bool selected = false;
    public bool locked = false;
    private Image[] panelRenderers;

    private void Start() {
        panelRenderers = panel.GetComponentsInChildren<Image>();
    }

    private Color GetPanelColor() {
        if(locked) return Color.gray;
        if(selected) return Color.red;

        return Color.white;
    }

    private void Update() {
        itemRenderer.sprite = itemSprite;

        Color panelColor = GetPanelColor();
        foreach(Image panelRenderer in panelRenderers) {
            panelRenderer.color = panelColor;
        }

        if(itemCount > 1 && !locked) {
            itemCountText.text = "" + itemCount;
        } else {
            itemCountText.text = "";
        }
    }
}
