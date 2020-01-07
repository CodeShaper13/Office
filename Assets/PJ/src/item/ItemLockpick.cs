using UnityEngine;

public class ItemLockpick : ItemBase<ItemDataLockPick> {

    private float pickTimer;
    private DoorBase door;

    public override void updateItemInHand(Player player) {
        base.updateItemInHand(player);

        if(this.isPickingLock()) {
            this.pickTimer -= Time.deltaTime;
        }
    }

    public override EnumInputBlock getInputBlock(Player player) {
        return this.isPickingLock() ? EnumInputBlock.ALL : EnumInputBlock.NONE;
    }

    public override void onRightClick(Player player) {
        base.onRightClick(player);

        if(this.isPickingLock()) {
            this.door = null;
            this.pickTimer = 0;
        } else {
            if(this.pickTimer <= 0) {
                Camera c = player.getCamera();
                RaycastHit hit;
                if(player.raycast(out hit, this.data.reach)) {
                    DoorBase door = hit.transform.GetComponent<DoorBase>();
                    if(door != null) {
                        if(this.door.isLocked) {
                            this.pickTimer = this.data.pickTime;
                            this.door = door;
                        }
                    }
                }
            }
        }
    }

    private bool isPickingLock() {
        return this.pickTimer > 0;
    }
}
