using UnityEngine;
using System.Linq;

public class TaskAttack : TaskBase {

    private const float HIT_RATE = 1.2f;
    private const float ATTACK_DISTANCE = 2f;
    private const int DAMAGE_AMOUNT = 10;

    private float lastHitTime;
    private bool attacking;
    private Player target;

    public TaskAttack(ZombieBase monster) : base(monster) { }

    public override bool preform() {
        if(this.target == null) {
            // Find a target.

            // Get the closest player
            this.target = Main.singleton.getPlayerManager().getAllPlayers<Player>().OrderBy(
                x => Vector2.Distance(this.monster.transform.position, x.transform.position)
                ).ToList()[0];
        }

        if(this.target != null) {
            this.navHelper.setDest(this.target.transform.position, ATTACK_DISTANCE);

            this.attacking = this.inRangeToAttack();

            if(this.attacking && Time.time > this.lastHitTime + HIT_RATE) {
                this.target.health.damage(DAMAGE_AMOUNT);
                this.lastHitTime = Time.time;
            }

            return true;
        }

        return false; // No target, go back to idle.
    }

    public override void updateAnimator(Animator animator) {
        base.updateAnimator(animator);

        animator.SetInteger("state", this.isAttacking() ? 2 : 1);
    }

    public bool isAttacking() {
        return attacking;
    }

    private bool inRangeToAttack() {
        return Vector3.Distance(
            this.monster.transform.position,
            this.target.transform.position) <= ATTACK_DISTANCE;
    }
}
