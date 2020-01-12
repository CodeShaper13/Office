using UnityEngine;

public class ItemFlare : ItemThrowable<ItemDataFlare> {

    [SerializeField]
    private Light lightEffect = null;
    [SerializeField]
    private ParticleSystem ps = null;

    private float litTime;
    private bool isLit;
    private bool burnedOut;

    private void OnDrawGizmos() {
        if(this.isFlareLit()) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, this.data.zombieAttractRange);
        }
    }

    public override void updateItemInWorld() {
        base.updateItemInWorld();

        if(this.isLit) {
            // Set all zombie within that range to go after the flare.
            foreach(ZombieBase zombie in GameObject.FindObjectsOfType<ZombieBase>()) {
                if(zombie.getTask() is TaskAttack && ((TaskAttack)zombie.getTask()).getTarget() == this.transform) {
                    continue; // Zombie is already moving towards the flare.
                }

                if(Vector3.Distance(this.transform.position, zombie.transform.position) <= this.data.zombieAttractRange) {
                    zombie.setTask(new TaskAttack(zombie));
                    ((TaskAttack)zombie.getTask()).setTarget(this.transform);
                }
            }

            // Decrease lit time.
            this.litTime -= Time.deltaTime;

            if(this.litTime <= 0) {
                // Flare burned out.
                this.burnedOut = true;
                this.isLit = false;
                if(this.lightEffect != null) {
                    this.lightEffect.enabled = false;
                }
                if(this.ps != null) {
                    this.ps.Stop();
                    this.ps.transform.SetParent(null);
                    GameObject.Destroy(ps.gameObject, 5f); // TODO shorten to optimize
                }

                // Tell the zombies to go back to hunting players.
                foreach(ZombieBase zombie in GameObject.FindObjectsOfType<ZombieBase>()) {
                    TaskAttack task;
                    if(zombie.getTask() is TaskAttack) {
                        task = (TaskAttack)zombie.getTask();
                        if(task.getTarget() == this.transform) {
                            task.setTarget(null);
                        }
                    }
                }
            }
        }
    }

    public override void onLeftClick(Player player) {
        base.onLeftClick(player);

        if(this.isLit) {
            this.throwItem(player);
            Rigidbody rb = this.GetComponent<Rigidbody>();
            rb.AddTorque(150, 0, 0, ForceMode.Acceleration);
        }
    }

    public override void onRightClick(Player player) {
        base.onRightClick(player);

        if(!this.isFlareLit()) {
            this.isLit = true;
            this.litTime = this.data.litTime;
            if(this.lightEffect != null) {
                this.lightEffect.enabled = true;
            }
            if(this.ps != null) {
                this.ps.Play();
            }
        }
    }

    public override bool canPickUpItem(Player player) {
        return !this.burnedOut;
    }

    public override string getExtraText(Player player) {
        return this.isLit ? "Lit" : base.getExtraText(player);
    }

    public bool isFlareLit() {
        return this.isLit;
    }
}
