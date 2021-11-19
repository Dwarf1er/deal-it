using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour {
    private List<ItemStack> itemStacks;
    private int selectedItem;

    private void Awake() {
        this.itemStacks = new List<ItemStack>();
    }

    public void AddItemStack(ItemStack itemStack) {
        itemStacks.Add(itemStack);
    }

    public List<ItemStack> GetItemStacks() {
        return itemStacks;
    }

    public int GetSelected() {
        return selectedItem;
    }

    public void ToggleItem(bool isRight) {
        int i = selectedItem;

        while(true) {
            if(isRight) {
                i = (i + 1) % itemStacks.Count;
            } else {
                if(--i < 0) i += itemStacks.Count;
            }

            if(!itemStacks[i].locked) {
                selectedItem = i;
                break;
            }                
        }
    }
}
