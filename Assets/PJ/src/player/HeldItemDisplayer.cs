using UnityEngine;

public class HeldItemDisplayer {

    private Player player;
    private Transform rightHand;
    private Transform leftHand;

    public IItem heldItemLastFrame;

    public HeldItemDisplayer(Player player, Transform rightHand, Transform leftHand) {
        this.player = player;
        this.rightHand = rightHand;
        this.leftHand = leftHand;
    }

    public void updateHeldItem(IItem item) {
        if(this.heldItemLastFrame != item) {
            // Something differend is now in hand

            if(this.heldItemLastFrame != null && this.heldItemLastFrame.getTransform() != null && !this.heldItemLastFrame.isInWorld()) {
                // Hide the old item
                this.heldItemLastFrame.hideItem();

            }

            this.heldItemLastFrame = item;
            
            if(this.heldItemLastFrame != null) {
                // Show the new held item
                this.heldItemLastFrame.showItem();

                // Put it in the players hand
                Transform t = this.heldItemLastFrame.getTransform();
                t.parent = this.rightHand;
                ItemData data = this.heldItemLastFrame.getData();
                t.localPosition = data.inHandPosition;
                t.localRotation = Quaternion.Euler(data.inHandRotation);
            }
        }



        // Temp for dev
        if(this.heldItemLastFrame != null) {
            Transform t = this.heldItemLastFrame.getTransform();
            ItemData data = this.heldItemLastFrame.getData();
            t.localPosition = data.inHandPosition;
            t.localRotation = Quaternion.Euler(data.inHandRotation);
            t.localScale = Vector3.one * data.inHandScale;
        }
    }
}
