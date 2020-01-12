using UnityEngine;

public class Container : MonoBehaviour {
    
    [SerializeField]
    private Slot[] slots;

    private ContainerContents<IItemBase> contents;
    /// <summary> The player who opened the container. </summary>
    private Player player;

    private void Start() {
        // Set the index field of all the slots.
        for(int i = 0; i < this.slots.Length; i++) {
            Slot s = this.slots[i];
            if(s != null) {
                s.setFields(i, this);
            }
        }
    }

    /// <summary>
    /// Called when the container is opened by a Player.
    /// </summary>
    public virtual Container onOpen(ContainerContents<IItemBase> data, Player player) {
        this.contents = data;
        this.player = player;

        this.gameObject.SetActive(true);

        return this;
    }

    /// <summary>
    /// Called when the container is closed for any reason.
    /// </summary>
    public virtual void onClose() {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Called every frame to render the item's within the container..
    /// </summary>
    public virtual void renderContents() {
        foreach(Slot slot in this.slots) {
            slot.renderSlotContents();
        }
    }

    /// <summary>
    /// Returns the ContainerContents that stores the contents of the container.
    /// </summary>
    public ContainerContents<IItemBase> getContents() {
        return this.contents;
    }

    /// <summary>
    /// Called by a slot game object when it is clicked on.
    /// </summary>
    public virtual void onSlotClick(int i, bool leftBtn, bool rightBtn, bool middleBtn) {
        ContainerHeldItem cm = this.player.playerUI.heldItem;
        IItemBase heldStack = cm.getHeldItem();
        IItemBase slotContents = this.contents.getItem(i);

        if(leftBtn) {
            if(heldStack == null && slotContents != null) {
                // Slot is empty, hand is occupied.  Pick up the stack.
                cm.setHeldStack(slotContents);
                this.contents.setItem(i, null);
            }
            else if(heldStack != null && slotContents == null) {
                // Slot is occupied, hand is not.  Drop off the stack.
                this.contents.setItem(i, heldStack);
                cm.setHeldStack(null);
            }
            else if(heldStack != null && slotContents != null) {
                // Both hand and slot have stuff.

                // Swap.
                IItemBase temp = slotContents;
                this.contents.setItem(i, heldStack);
                cm.setHeldStack(temp);
            }
        }
    }
}