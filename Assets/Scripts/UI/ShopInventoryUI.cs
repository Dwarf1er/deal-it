using System.Collections;
using UnityEngine;

public class ShopInventoryUI : InventoryUI {
    private ShopInventory shopInventory;

    protected override void Start() {
        base.Start();
        EventManager.Get()
            .Subscribe((OpenInventoryEvent inventoryEvent) => OnInventoryOpen(inventoryEvent));
    }

    protected override Inventory GetInventory() {
        return shopInventory;
    }

    private void OnInventoryOpen(OpenInventoryEvent inventoryEvent) {
        if(!(inventoryEvent.GetInventory() is ShopInventory)) return;

        this.shopInventory = (ShopInventory)inventoryEvent.GetInventory();
        SetVisible(true);
    }
}
