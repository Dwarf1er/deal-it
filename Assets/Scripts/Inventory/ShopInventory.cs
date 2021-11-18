using UnityEngine;

public class ShopInventory : Inventory {
    [SerializeField] private Sprite sprite;

    private void Start() {
        this.AddItemStack(new ItemStack(new Item(sprite), 1));
        this.AddItemStack(new ItemStack(new Item(sprite), 2));
        this.AddItemStack(new ItemStack(new Item(sprite), 3));
    }
}
