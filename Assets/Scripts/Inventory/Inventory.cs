using System.Collections.Generic;

public abstract class Inventory {
    private List<ItemStack> itemStacks;

    public Inventory() {
        this.itemStacks = new List<ItemStack>();
    }

    public void AddItemStack(ItemStack itemStack) {
        itemStacks.Add(itemStack);
    }

    public List<ItemStack> GetItemStacks() {
        return itemStacks;
    }
}
