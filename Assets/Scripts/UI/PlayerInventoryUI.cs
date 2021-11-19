using System.Collections;
using UnityEngine;

public class PlayerInventoryUI : InventoryUI {
    private PlayerInventory playerInventory;

    protected override void Start() {
        base.Start();
        EventManager.Get()
            .Subscribe((OpenInventoryEvent inventoryEvent) => OnInventoryOpen(inventoryEvent));
    }

    protected override Inventory GetInventory() {
        return playerInventory;
    }

    private void OnInventoryOpen(OpenInventoryEvent inventoryEvent) {
        if(!(inventoryEvent.GetInventory() is PlayerInventory)) return;

        this.playerInventory = (PlayerInventory)inventoryEvent.GetInventory();
        SetVisible(true);
    }
}
