using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class CharacterNonPlayable : Character {

    protected Animator anim;
    public NavHelper navHelper;

    protected override void init() {
        base.init();

        this.navHelper = new NavHelper(this);
        this.anim = this.GetComponent<Animator>();

        // Zombies are randomly picked to mirror their animation to make so me variety.
        this.anim.SetBool("mirror", Random.Range(0, 2) == 1);
    }
}
