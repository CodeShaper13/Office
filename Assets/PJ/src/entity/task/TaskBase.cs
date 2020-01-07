using UnityEngine;
using UnityEngine.AI;

public abstract class TaskBase {

    protected ZombieBase monster;
    protected NavHelper navHelper;

    public TaskBase(ZombieBase monster) {
        this.monster = monster;
        this.navHelper = monster.navHelper;
    }

    public abstract bool preform();

    public virtual void updateAnimator(Animator animator) { }

    public virtual void onDamage(Player player) { }

    public virtual void onFinish() { }
}
