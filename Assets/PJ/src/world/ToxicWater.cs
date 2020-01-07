using UnityEngine;

public class ToxicWater : MonoBehaviour {

    [Header("How often the player is damage in seconds")]
    public float damageRate;
    [Header("How much damage is delt")]
    public int amount;

    private float lastDamage;

    private void Update() {
        //TODO make work for multiple players.
        /*
        if(!Pause.isPaused()) {
            float pY = Player.singleton.getFootPos().y;
            if(pY < this.transform.position.y) {
                if(Time.time > this.lastDamage + this.damageRate) {
                    Player.singleton.damage(new DamageSource(this.amount * pY < this.transform.position.y - 1.25f ? 10 : 1, null));
                    this.lastDamage = Time.time;
                }
            }
        }
        */
    }
}
