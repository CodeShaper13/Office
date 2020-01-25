using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TaskAttack : TaskBase {

    private float lastHitTime;
    private bool isAttacking;
    private Transform target;

    public TaskAttack(ZombieBase monster) : base(monster) { }

    public override bool preform() {
        if(this.target == null) {
            // Find a target.

            // No set target, search for a new one.
            Transform target = this.findTarget();

            if(target != null) {
                this.setTarget(target);
            } else {
                return false;
            }
        }

        if(this.target != null) {
            this.navHelper.setDest(this.target.transform.position, this.zombie.stats.attackReach);

            if(this.inRangeToAttack() && this.isTargetDamageable()) {
                this.isAttacking = true;

                if(this.isAttacking && Time.time > this.lastHitTime + this.zombie.stats.attackRate) {
                    Health hp = this.target.GetComponent<Health>();
                    if(hp != null) {
                        bool dead = hp.damage(this.zombie.stats.attackDamageAmount);
                        this.lastHitTime = Time.time;

                        if(dead) {
                            this.setTarget(null);
                        }
                    }
                }
            } else {
                this.isAttacking = false;
            }

            return true;
        }

        return false; // No target, go back to idle.
    }

    public override void updateAnimator(Animator animator) {
        base.updateAnimator(animator);

        animator.SetInteger("state", this.isAttackingTarget() ? 2 : 1);
    }

    /// <summary>
    /// Returns the task's target.  May return null.
    /// </summary>
    public Transform getTarget() {
        return this.target;
    }

    /// <summary>
    /// Sets the task's target.  Pass null for no target.
    /// </summary>
    public void setTarget(Transform target) {
        this.target = target;
    }

    /// <summary>
    /// Returns true if the zomibe is in the act of attacking.
    /// </summary>
    public bool isAttackingTarget() {
        return this.isAttacking;
    }

    /// <summary>
    /// Checks if the target is something that should be hit (like a player).  Object, like flares, are not hit.
    /// </summary>
    private bool isTargetDamageable() {
        return this.target.transform.GetComponent<Health>() != null;
    }

    /// <summary>
    /// Returns true if the zombie is close enought to the target to hit it.
    /// </summary>
    private bool inRangeToAttack() {
        return Vector3.Distance(
            this.zombie.transform.position,
            this.target.transform.position) <= this.zombie.stats.attackReach;
    }

    /// <summary>
    /// Finds a target for the zombie.  Null is returned if no target can be found.
    /// </summary>
    private Transform findTarget() {
        List<Character> chars = new List<Character>(GameObject.FindObjectsOfType<Character>());
        chars.RemoveAll(character => character is ZombieBase || character.health.isDead());
        chars = chars.OrderBy(x => Vector3.Distance(x.transform.position, this.zombie.transform.position)).ToList();

        if(chars.Count > 0) {
            return chars[0].transform;
        } else {
            return null;
        }
    }
}
