using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour {
    [SerializeField] private Image itemRenderer;
    [SerializeField] private Transform panel;
    [SerializeField] private Text itemCountText;
    private ItemStack itemStack;
    public bool selected;
    private Image[] panelRenderers;

    private void Start() {
        panelRenderers = panel.GetComponentsInChildren<Image>();
    }

    public void SetItemStack(ItemStack itemStack) {
        this.itemStack = itemStack;
    }

    private Color GetPanelColor() {
        if(itemStack == null || itemStack.locked) return Color.gray;
        if(selected) return Color.red;

        return Color.white;
    }

    private void Update() {
        Color panelColor = GetPanelColor();
        foreach(Image panelRenderer in panelRenderers) {
            panelRenderer.color = panelColor;
        }

        Color color = itemRenderer.color;
        if(itemStack == null) {
            color.a = 0;
            itemRenderer.color = color;
        } else {
            color.a = 1;
            itemRenderer.color = color;
            itemRenderer.sprite = itemStack.item.sprite;

            if(itemStack.count > 1 && !itemStack.locked) {
                itemCountText.text = "" + itemStack.count;
            } else {
                itemCountText.text = "";
            }
        }
    }
}
