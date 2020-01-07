using UnityEngine;

public class TaskIdle : TaskBase {

    private float timeToLook;
    private Quaternion lookDirection;

    public TaskIdle(ZombieBase monster) : base(monster) {
        //if(Director.get().dayNightCycle.isNight()) {
            this.monster.setTask(new TaskAttack(this.monster));
            return;
        //}

        // Place monster onto NavMesh.
        //ssthis.agent.SetDestination(this.monster.transform.position);
        //this.agent.path.cl
    }

    public override bool preform() {
        return true;
    }

    public override void onDamage(Player player) {
        this.monster.setTask(new TaskAttack(this.monster));
    }

    public override void updateAnimator(Animator animator) {
        base.updateAnimator(animator);

        animator.SetInteger("state", 0); // Idle animation.
    }
}
