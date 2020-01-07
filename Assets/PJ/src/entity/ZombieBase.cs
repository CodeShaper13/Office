using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class ZombieBase : MonoBehaviour {
    
    public Health health;
    private Animator anim;

    public NavHelper navHelper;
    protected TaskBase task;

    private void Awake() {
        this.navHelper = new NavHelper(this);
        this.anim = this.GetComponent<Animator>();

        // Zombies are randomly picked to mirror their animation to make so me variety.
        this.anim.SetBool("mirror", Random.Range(0, 1) == 1);

        this.init();
    }

    private void Start() {
        this.health.setHealth(100);
        this.health.subscribeToDamageEvent(onDamageCallback);

        // Set task to Idle if no task was set shortly after GameObject was instantiated
        if(this.task == null) {
            this.setTask(new TaskAttack(this));
        }
    }

    private void onDamageCallback(int amount) {
        if(this.health.isDead()) {
            this.GetComponent<Ragdoll>().makeFloppy(true, true);
        }
    }

    private void Update() {
        if(!Pause.isPaused() && !this.health.isDead()) {
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
                i = ((TaskAttack)this.task).isAttacking() ? 2 : 0;
            }

            this.update();
        }
    }

    protected virtual void init() { }

    protected virtual void update() { }

    public void setTask(TaskBase newTask) {
        if(this.task != null) {
            this.task.onFinish();
        }

        this.task = (newTask == null) ? new TaskIdle(this) : newTask;
    }

    public TaskBase getTask() {
        return this.task;
    }

    public void onShoot(DamageSource source, RaycastHit hit) {
        if(this.health.damage(source.amount)) {
            this.GetComponent<Ragdoll>().makeFloppy(true, true);
        }
    }
}
