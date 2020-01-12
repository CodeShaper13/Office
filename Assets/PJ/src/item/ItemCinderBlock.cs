using UnityEngine;

public class ItemCinderBlock : ItemThrowable<ItemDataCinderBlock> {

    /// <summary> True if the cinder block has hit a target.  when this item is thrown, the field is reset. </summary>
    private bool hitTarget;

    private void OnCollisionEnter(Collision collision) {
        print("Hit " + collision.gameObject.name);

        if(!this.hitTarget) {
            Health health = collision.transform.GetComponentInParent<Health>();
            if(health != null) {
                health.damage(this.data.damage);
                this.hitTarget = true;
            }
        }
    }

    public override void onLeftClick(Player player) {
        base.onLeftClick(player);

        this.throwItem(player);
        this.hitTarget = false;
    }

    public override bool canPickUpItem(Player player) {
        return base.canPickUpItem(player);
    }
}
