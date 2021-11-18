using System.Collections;
using UnityEngine;

public class ShopInventoryUI : InventoryUI {
    private ShopInventory shopInventory;

    protected override void Start() {
        base.Start();
        EventManager.Get()
            .Subscribe((OpenInventoryEvent inventoryEvent) => OnInventoryOpen(inventoryEvent));
    }

    private void OnInventoryOpen(OpenInventoryEvent inventoryEvent) {
        if(!(inventoryEvent.GetInventory() is ShopInventory)) return;

        this.shopInventory = (ShopInventory)inventoryEvent.GetInventory();
        SetVisible(true);
    }

    private void OnInventoryClose(CloseInventoryEvent inventoryEvent) {
        
    }

    private void Update() {
        if(Visible() && Input.GetKeyDown(KeyCode.N)) {
            ToggleItem();
        }
    }
}
