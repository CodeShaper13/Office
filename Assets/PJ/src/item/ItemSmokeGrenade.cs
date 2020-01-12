using UnityEngine;

public class ItemSmokeGrenade : ItemThrowable<ItemDataSmokeGrenade> {

    [SerializeField]
    private ParticleSystem ps = null;

    private float timer;
    private bool smoking;

    private void OnDrawGizmos() {
        if(this.isSmoking()) {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(this.transform.position, this.data.smokeRadius);
        }
    }

    public override void updateItemInWorld() {
        base.updateItemInWorld();

        if(this.isSmoking()) {
            this.timer -= Time.deltaTime;

            if(this.timer <= 0) {
                this.smoking = false;
                if(this.ps != null) {
                    this.ps.Stop();
                    this.ps.transform.SetParent(null);
                    GameObject.Destroy(ps.gameObject, 5f); // TODO shorten to optimize

                }

                ItemManager.destroy(this);
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

        if(!this.isSmoking()) {
            this.smoking = true;
            this.timer = this.data.smokeTime;

            if(this.ps != null) {
                this.ps.Play();
            }

            // TODO play sound effect.
        }
    }

    public override bool canPickUpItem(Player player) {
        return !this.isSmoking();
    }

    public override string getExtraText(Player player) {
        return this.isSmoking() ? "Smoking" : base.getExtraText(player);
    }

    /// <summary>
    /// Returns true if the grenade is smoking.
    /// </summary>
    public bool isSmoking() {
        return this.smoking;
    }

    /// <summary>
    /// Return's true if the point is in the smoke cloud.
    /// </summary>
    public bool isInSmokeCloud(Vector3 point) {
        return this.isSmoking() && Vector3.Distance(this.transform.position, point) <= this.data.smokeRadius;
    }
}
