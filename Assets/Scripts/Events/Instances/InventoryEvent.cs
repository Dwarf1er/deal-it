using UnityEngine;

public class VisibilityInventoryEvent : IEvent {
    private Inventory inventory;

    public VisibilityInventoryEvent(Inventory inventory) {
        this.inventory = inventory;
    }

    public Inventory GetInventory() {
        return inventory;
    }

    public Vector2 GetPosition() {
        return Vector2.zero;
    }

    public float GetRange() {
        return 0;
    }
}

public class OpenInventoryEvent : VisibilityInventoryEvent {
    public OpenInventoryEvent(Inventory inventory) : base(inventory) {}
}

public class CloseInventoryEvent : VisibilityInventoryEvent {
    public CloseInventoryEvent(Inventory inventory) : base(inventory) {}
}
