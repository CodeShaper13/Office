using UnityEngine;

public class ZombieRagdollPart : RagdollPart {

    [SerializeField]
    private float damageMultiplyer = 1f;

    public void onShoot(DamageSource source, RaycastHit hit) {
        source.amount = (int)(source.amount * this.damageMultiplyer);
        this.GetComponentInParent<ZombieBase>().onShoot(source, hit);
        this.rigidbodyComponent.AddForce(hit.normal * -1 * Random.Range(9, 14), ForceMode.Impulse);
    }
}
