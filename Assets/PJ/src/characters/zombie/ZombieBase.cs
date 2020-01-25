using UnityEngine;

public class ZombieBase : CharacterNonPlayable {

    [Space]

    [SerializeField]
    [Tooltip("How many seconds the dead zombie model is visible until it's destroyed.")]
    private float timeUntilModelHide = 30f;

    public ZombieStats stats;
    protected TaskBase task;

    /// <summary> Seconds the zombie has been dead. </summary>
    private float timeDead;

    protected override void init() {
        base.init();

        this.health.setHealth(100);
        this.health.subscribeToDamageEvent(onDamageCallback);

        // Set task to Idle if no task was set shortly after GameObject was instantiated
        if(this.task == null) {
            this.setTask(new TaskAttack(this));
        }
    }

    protected override void update() {
        base.update();

        if(!this.health.isDead()) {
            this.navHelper.update();

            bool continueExecuting = this.task.preform();
            if(!continueExecuting) {
                this.setTask(null);
            }

            this.task.updateAnimator(this.anim);

            // TODO is the attacking player when in front of a door?
            int i = 1;
            if(this.navHelper.door != null) {
                i = 2;
            }
            else if(this.task is TaskAttack) {
                i = ((TaskAttack)this.task).isAttackingTarget() ? 2 : 0;
            }
        } else {
            this.timeDead += Time.deltaTime;
            if(this.timeDead > this.timeUntilModelHide) {
                GameObject.Destroy(this.gameObject);
            }
        }
    }

    private void onDamageCallback(int amoun, RaycastHit? hit) {
        this.task.onDamage();
    }

    public void setTask(TaskBase newTask) {
        if(this.task != null) {
            this.task.onFinish();
        }

        this.task = (newTask == null) ? new TaskIdle(this) : newTask;
    }

    public TaskBase getTask() {
        return this.task;
    }
}
