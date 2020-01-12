using UnityEngine;

public class TaskIdle : TaskBase {

    private float timeToLook;
    private Quaternion lookDirection;

    public TaskIdle(ZombieBase monster) : base(monster) {
        //if(Director.get().dayNightCycle.isNight()) {
            this.zombie.setTask(new TaskAttack(this.zombie));
            return;
        //}

        // Place monster onto NavMesh.
        //ssthis.agent.SetDestination(this.monster.transform.position);
        //this.agent.path.cl
    }

    public override bool preform() {
        return true;
    }

    public override void onDamage() {
        this.zombie.setTask(new TaskAttack(this.zombie));
    }

    public override void updateAnimator(Animator animator) {
        base.updateAnimator(animator);

        animator.SetInteger("state", 0); // Idle animation.
    }
}
