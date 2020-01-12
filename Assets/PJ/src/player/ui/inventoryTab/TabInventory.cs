using UnityEngine;

public class TabInventory : TabBase {

    [SerializeField]
    private ContainerInventory containerInventory;

    private void Awake() {
        
    }

    public override void onUpdate() {
        base.onUpdate();

        // Render the item held by the mouse.
        this.getPlayer().playerUI.heldItem.renderHeldItem();

        // Render the contents of the container
        this.containerInventory.renderContents();
    }

    public override void onTabOpen() {
        base.onTabOpen();

        Player p = this.getPlayer();
        this.containerInventory.onOpen(p.inventory, p);
    }

    public override void onTabClose() {
        base.onTabClose();

        // Returns the held item to the players inventory.
        ContainerHeldItem hi = this.getPlayer().playerUI.heldItem;
        if(hi.getHeldItem() != null) {
            this.getPlayer().inventory.addItem((IItem)hi.getHeldItem());
            hi.setHeldStack(null);
        }
    }
}
