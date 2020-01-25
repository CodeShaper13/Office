using UnityEngine;

public class CharacterInHandDisplayer : MonoBehaviour {

    [SerializeField]
    private Transform rightHand;
    [SerializeField]
    private Transform leftHand;
    [SerializeField]
    private Character character;

    private EnumDominateHand dominateHand;

    private IItem itemLastFrame;

    private void Awake() {
        this.dominateHand = Random.Range(0, 1) > 0.1f ? EnumDominateHand.RIGHT : EnumDominateHand.LEFT;
    }

    private void Update() {
        IItem item = this.character.getHeldItem();
        
        if(item != null) {
            // character is holding an item.
            Transform t = item.getTransform();

            if(t.parent != this.getHandTransform()) {
                // It hasn't gotten moved to a hand yet, move it.
                item.showItem();

                t.parent = this.getHandTransform();
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
            }
        } else {
            // Item is null
        }
    }

    public EnumDominateHand getDominateHand() {
        return this.dominateHand;
    }

    private Transform getHandTransform() {
        return this.dominateHand == EnumDominateHand.RIGHT ? this.rightHand : this.leftHand;
    }

    public enum EnumDominateHand {
        RIGHT = 0,
        LEFT = 1,
    }
}
