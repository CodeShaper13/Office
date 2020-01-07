using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ItemGrenade : ItemBase<ItemDataGrenade> {

    [SerializeField]
    private GameObject explosionEffectPrefab = null;

    private bool primed;
    private float timer;

    public override void updateItemInWorld() {
        base.updateItemInWorld();

        if(this.primed) {
            this.timer -= Time.deltaTime;

            if(this.timer <= 0) {
                this.explode();
            }
        }
    }

    public override void onLeftClick(Player player) {
        base.onLeftClick(player);

        this.throwGrenade(player);
    }

    public override void onRightClick(Player player) {
        base.onRightClick(player);

        if(!this.primed) {
            this.primeGrenade(player);
        }
    }

    public override string getExtraText(Player player) {
        return this.primed ? "Primed!" : base.getExtraText(player);
    }

    private void primeGrenade(Player player) {
        this.primed = true;
        this.timer = this.data.explodeTime;

        // TODO play sound effect.
    }

    private void throwGrenade(Player player) {
        player.inventory.setItem(player.hotbarIndex.get(), null);
        Transform camTrans = player.getCamera().transform;
        this.setInWorld(true, camTrans.position + (camTrans.forward * 0.5f), camTrans.rotation);

        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(camTrans.forward * data.throwForce, ForceMode.Impulse);  // TODO make more realistic?

        BoxCollider col = this.GetComponent<BoxCollider>();
        col.enabled = true;

        // TODO play sound effect.
    }

    private void explode() {
        // Spawn the particle effect.
        Transform t = GameObject.Instantiate(this.explosionEffectPrefab).transform;
        t.position = this.transform.position;
        t.localScale = Vector3.one * 0.5f;
         
        DebugDrawer.box(this.transform.position, Vector3.one * this.data.explosionRadius, Colors.orange, 10000);

        List<Rigidbody> rbs = new List<Rigidbody>();

        // Damage everything in the area.
        Collider[] cols = Physics.OverlapSphere(this.transform.position, this.data.explosionRadius);
        foreach(Collider col in cols) {
            if(col.gameObject == this.gameObject) {
                continue;
            }

            // Damage anything with a health component.
            Health health = col.transform.GetComponentInParent<Health>();
            if(health != null) {
                health.damage(this.data.explosionDamage);
            }
            
            // Apply force to any rigidbodies that belong to something dead.
            Rigidbody rb = col.transform.GetComponent<Rigidbody>();
            if(rb != null) {
                health = col.transform.GetComponentInParent<Health>();
                if(health != null && health.isDead()) {
                    rbs.Add(rb);
                }
            }
        }

        // Break the joins.
        foreach(Rigidbody rb in rbs) {
            CharacterJoint join = rb.transform.GetComponent<CharacterJoint>();
            if(join != null) {
                GameObject.Destroy(join);
            }
        }
         
        // Apply force.
        foreach(Rigidbody rb in rbs) {
            Vector3 direction = rb.transform.position - this.transform.position;
            rb.AddForce(direction * this.data.rigidbodyForce, this.data.forceMode);
        }

        // Destroy this Item
        ItemManager.destroy(this);
    }
}
