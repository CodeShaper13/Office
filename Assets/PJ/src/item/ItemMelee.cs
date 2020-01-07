using UnityEngine;

public class ItemMelee : ItemBase<ItemDataMelee> {

    private float timer;

    public override void updateItemInHand(Player player) {
        base.updateItemInHand(player);

        if(this.timer > 0) {
            this.timer -= Time.deltaTime;
        }
    }

    public override void onLeftClick(Player player) {
        if(this.timer <= 0) {
            //this.attackInFront();

            RaycastHit hit;
            if(player.raycast(out hit, this.data.range)) {
                Health hp = hit.collider.GetComponent<Health>();
                if(hp != null) {
                    hp.damage(this.data.damage);
                }
            }


            // Even if you miss, there is still a cooldown.
            this.timer = this.data.attackRate;
        }
    }

    // Only ofr third person
    private void attackInFront() {
        int attackRange = 30; // Degrees from the line facing forward.

        Collider[] cols = Physics.OverlapSphere(this.transform.position, this.data.range);
        Vector3 characterToCollider;
        float dot;
        foreach(Collider collider in cols) {
            characterToCollider = (collider.transform.position - this.transform.position).normalized;
            dot = Vector3.Dot(characterToCollider, this.transform.forward);

            if(Vector2.Distance(new Vector2(this.transform.position.x, this.transform.position.z), new Vector2(collider.transform.position.x, collider.transform.position.z)) < 0.75f || dot >= Mathf.Cos(attackRange)) {
                // Object hit!
                Health hp = collider.GetComponent<Health>();
                if(hp != null) {
                   hp.damage(this.data.damage);
                }
            }
        }
    }
}
