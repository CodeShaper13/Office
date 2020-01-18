using UnityEngine;

[RequireComponent(typeof(Health))]
public class Character : MonoBehaviour {

    public Health health;

    private void Awake() {

    }

    private void Start() {
        this.health.subscribeToDamageEvent(onDamageCallback);

        this.init();
    }

    private void Update() {
        this.update();
    }

    protected virtual void init() {
    }

    protected virtual void update() {
    }

    private void onDamageCallback(int amount) {
        if(this.health.isDead()) {
            Ragdoll doll = this.GetComponent<Ragdoll>();
            if(doll != null) {
                this.GetComponent<Ragdoll>().makeFloppy(true, true);
            }
        }
    }
}
