using UnityEngine;
using UnityEngine.AI;

public class ZombieBase : CharacterNonPlayable {
    
    public ZombieStats stats;
    protected TaskBase task;

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
        }
    }

    private void onDamageCallback(int amount) {
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
