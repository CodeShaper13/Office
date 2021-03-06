﻿using System.Collections.Generic;
using UnityEngine;

public class ItemGrenade : ItemThrowable<ItemDataGrenade> {

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

        this.throwItem(player);

        //TODO play sound effect.
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

    public override bool canPickUpItem(Player player) {
        return !this.primed;
    }

    private void primeGrenade(Player player) {
        this.primed = true;
        this.timer = this.data.explodeTime;

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
            
            // Add to the list of effected rigidbodies.
            Rigidbody rb = col.transform.GetComponent<Rigidbody>();
            if(rb != null) {
                health = col.transform.GetComponentInParent<Health>();
                if(health != null && health.isDead()) {
                    rbs.Add(rb);
                }
            }
        }

        // Break the joins of the effected rigidbodies.
        foreach(Rigidbody rb in rbs) {
            CharacterJoint join = rb.transform.GetComponent<CharacterJoint>();
            if(join != null) {
                GameObject.Destroy(join);
            }
        }
         
        // Apply force tot he effected rigidbodies.
        foreach(Rigidbody rb in rbs) {
            Vector3 direction = rb.transform.position - this.transform.position;
            rb.AddForce(direction * this.data.rigidbodyForce, this.data.forceMode);
        }

        // Destroy this Item
        ItemManager.destroy(this);
    }
}
