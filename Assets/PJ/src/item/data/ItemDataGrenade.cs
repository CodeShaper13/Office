using UnityEngine;

[CreateAssetMenu(fileName = "ItemGrenade", menuName = "ZPanic/Items/ItemGrenade", order = 1)]
public class ItemDataGrenade : ItemData {

    [Space]

    [Tooltip("How much damage the grenage deals.")]
    public int explosionDamage;
    [Tooltip("How big the radius of explosion's damage area is.")]
    public float explosionRadius = 1f;
    [Tooltip("How far the grenade can be thrown.")]
    public float throwForce;
    [Tooltip("How long until the grenade explodes after being primed")]
    public float explodeTime;
    [Tooltip("How much force is put on physics objects.")]
    public float rigidbodyForce = 1f;
    [Tooltip("The force mode to use when adding force to physicis objects.")]
    public ForceMode forceMode = ForceMode.Impulse;
}
