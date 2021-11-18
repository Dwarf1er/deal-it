public class ItemStack {
    public Item item { get; }
    public int count { get; }

    public ItemStack(Item item, int count) {
        this.item = item;
        this.count = count;
    }
}
