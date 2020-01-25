using UnityEngine;

public class FirstPersonHeldItemDisplayer : MonoBehaviour {

    [SerializeField]
    private Transform rightHand;
    [SerializeField]
    private Transform leftHand;
    private Character character;

    public IItem heldItemLastFrame;

    private void Awake() {
        this.character = this.GetComponent<Character>();     
    }

    private void Update() {
        IItem item = this.character.getHeldItem();

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
