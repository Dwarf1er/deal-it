using UnityEngine;

public class CutsceneShop : CutsceneAbstract, ISubscriber {
    private ShopInventory shopInventory;
    private bool done;

    public override void Enter() {
        this.shopInventory = Object.FindObjectOfType<ShopInventory>();
        EventManager.Get().Broadcast(new OpenInventoryEvent(shopInventory));
        EventManager.Get().Subscribe((CloseInventoryEvent inventoryEvent) => OnInventoryClose(inventoryEvent));
        done = false;
    }

    public override bool Loop() {
        return !done;
    }

    private void OnInventoryClose(CloseInventoryEvent inventoryEvent) {
        if(inventoryEvent.GetInventory().Equals(shopInventory)) {
            done = true;
        }
    }

    public override void Exit() {
        EventManager.Get().UnSubcribeAll(this);
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return null;
    }
}
