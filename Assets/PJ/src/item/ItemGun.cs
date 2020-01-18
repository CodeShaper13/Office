using UnityEngine;

public class ItemGun : ItemBase<ItemDataGun> {

    private bool isReloading;
    private float timer;
    private int shotsLeft;

    public override void updateItemInHand(Player player) {
        base.updateItemInHand(player);

        this.timer += Time.deltaTime;

        // Update reloading.
        if(this.isReloading) {
            if(this.timer > this.data.reloadTime) {
                this.shotsLeft = this.data.bulletCount;
                this.isReloading = false;
            }
        }
    }

    public override void onRightClick(Player player) {
        base.onRightClick(player);

        if(this.shotsLeft > 0 && this.timer >= this.data.shootDelay) {
            this.shootGun(player);
        }
    }

    public override void onReloadPress(Player player) {
        base.onReloadPress(player);

        if(!this.isReloading && this.shotsLeft < this.data.bulletCount) {
            int index;
            if(player.inventory.containsItem(this.data.ammoItems, out index)) {
                ItemManager.destroyItem(player, index);

                this.timer = 0f;
                this.isReloading = true;
            }
        }
    }

    public override void animatorUpdate(Player player, Animator anim) {
        base.animatorUpdate(player, anim);

        anim.SetBool("Reload_b", this.isReloading);
    }

    public override string getExtraText(Player player) {
        return this.isReloading ? "Reloading" : this.shotsLeft + "/" + this.data.bulletCount;
    }

    public override EnumInputBlock getInputBlock(Player player) {
        return this.isReloading ? EnumInputBlock.MOVE | EnumInputBlock.CHANGE_HELD : EnumInputBlock.NONE;
    }

    private void shootGun(Player player) {
        //if(this.gunSoundSource != null) {
        //    this.gunSoundSource.Play();
        //}

        //this.muzzleFlash.Play();

        //if(this.muzzleFlashLight != null) {
        //    this.muzzleFlashLight.enabled = true;
        //    this.Invoke("hideFlash", 0.1f);
        //}

        this.shotsLeft--;

        this.timer = 0f;

        RaycastHit hit;

        // Third person code:
        //Vector3 orgin = player.transform.position + Vector3.up * 1.6f;
        //Vector3 forward = player.transform.forward;

        Vector3 orgin = player.getCamera().transform.position;
        Vector3 forward = player.getCamera().transform.forward;

        foreach(Quaternion bulletDirection in this.getBulletDirections(this.data)) {
            Ray ray = new Ray(orgin + forward, bulletDirection * forward);
            if(Physics.Raycast(ray, out hit)) {
                Debug.DrawLine(ray.origin, ray.GetPoint(100), Color.green, 1);

                ZombieRagdollPart part = hit.transform.GetComponent<ZombieRagdollPart>();

                // Damage the hit object.
                Health health = hit.transform.GetComponentInParent<Health>();
                if(health != null) {
                    float multiplyer = 1f;
                    if(part != null) {
                        multiplyer *= part.damageMultiplyer;
                    }
                    health.damage((int)(this.data.damageAmount * multiplyer));
                }

                if(part != null) {
                    Vector3 force = hit.normal * -1 * Random.Range(9, 14);
                    part.rigidbodyComponent.AddForce(force, ForceMode.Impulse);
                }

                /*
                if(hit.transform.CompareTag("Wood")) {
                    this.spawnBulletHitEffect(player.woodHitPrefab, hit);
                }
                else if(hit.transform.CompareTag("Metal")) {
                    this.spawnBulletHitEffect(player.metalHitPrefab, hit);
                }
                else if(hit.transform.CompareTag("Stone")) {
                    this.spawnBulletHitEffect(player.stoneHitPrefab, hit);
                }
                */
            }
            else {
                Debug.DrawRay(orgin + forward, forward * 100, Color.red, 1000);
            }
        }
    }

    private void hideFlash() {
        //this.muzzleFlashLight.enabled = false;
    }

    /// <summary>
    /// Spawns a bullet hit effect on an objct.
    /// </summary>
    private void spawnBulletHitEffect(GameObject prefab, RaycastHit hit) {
        GameObject spawnedDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
        spawnedDecal.transform.SetParent(hit.collider.transform);
        spawnedDecal.transform.RotateAround(hit.point, hit.normal, Random.Range(0, 360));
    }

    public virtual Quaternion[] getBulletDirections(ItemDataGun itemData) {
        Quaternion[] array = new Quaternion[itemData.bulletsPerShot];
        for(int i = 0; i < itemData.bulletsPerShot; i++) {
            array[i] = Quaternion.Euler(Random.Range(-itemData.bulletSpread, itemData.bulletSpread), 0, Random.Range(-itemData.bulletSpread, itemData.bulletSpread));
        }
        return array;
    }
}
