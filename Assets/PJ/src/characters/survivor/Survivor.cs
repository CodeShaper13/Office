using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(RandomSkin))]
[RequireComponent(typeof(Animator))]
public class Survivor : CharacterNonPlayable, IClickable<Player> {

    // Set by the GameModeSurvivorRescue object.
    [NonSerialized]
    public int skinIndex;
    /// <summary> The Player the egent is following. </summary>
    private Player playertoFollow;
    /// <summary> The time in seconds the survivor has been wiating for the player. </summary>
    private float timeWaiting;

    protected override void init() {
        base.init();

        this.navHelper.agent.stoppingDistance = 7f;

        this.anim.SetBool("Static_b", true);
    }

    protected override void update() {
        base.update();

        if(!this.health.isDead()) {
            if(this.playertoFollow != null) {
                this.navHelper.setDest(this.playertoFollow.getFootPos());

                float remainingDis = this.navHelper.agent.remainingDistance;
                float f;
                if(remainingDis >= this.navHelper.agent.stoppingDistance) {
                    this.setAnimRunning(true);
                } else {
                    this.setAnimRunning(false);
                }
            } else {
                this.timeWaiting += Time.deltaTime;

                // Play idle animation.
            }
        }
    }

    public bool onClick(Player player) {
        if(this.playertoFollow == null) {
            player.playerUI.setSubtitleText("survivor.message.followYou", 1f);
            this.setFollowing(player);
            this.navHelper.agent.isStopped = false;

        }
        else {
            player.playerUI.setSubtitleText("survivor.message.waitHere", 1f);
            this.setFollowing(null);
            this.navHelper.agent.isStopped = true;
            this.timeWaiting = 0;
        }

        return true;
    }

    private void setAnimRunning(bool running) {
        this.anim.SetFloat("Speed_f", running ? 1f : 0);
    }

    private void setFollowing(Player player) {
        this.playertoFollow = player;
    }
}
