using UnityEngine;

[CreateAssetMenu(fileName = "ItemGrenade", menuName = "ZPanic/Items/ItemGrenade", order = 1)]
public class ItemDataGrenade : ItemDataThrowable {

    [Space]

    [Tooltip("How much damage the grenage deals.")]
    public int explosionDamage;
    [Tooltip("How big the radius of explosion's damage area is.")]
    public float explosionRadius = 1f;
    [Tooltip("How long until the grenade explodes after being primed")]
    public float explodeTime;
}
