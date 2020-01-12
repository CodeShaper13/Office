using UnityEngine;

public abstract class TaskBase {

    protected ZombieBase zombie;
    protected NavHelper navHelper;

    public TaskBase(ZombieBase monster) {
        this.zombie = monster;
        this.navHelper = monster.navHelper;
    }

    public abstract bool preform();

    public virtual void updateAnimator(Animator animator) { }

    public virtual void onDamage() { }

    public virtual void onFinish() { }
}
