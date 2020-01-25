using UnityEngine;

[CreateAssetMenu(fileName = "ItemGun", menuName = "ZPanic/Items/ItemGun", order = 1)]
public class ItemDataGun : ItemData {

    [Space]

    [Tooltip("How long, in seconds, does it take to reload.")]
    public float reloadTime = 1f;
    [Tooltip("How many shots does the gun have before reloading")]
    public int bulletCount = 1;
    [Tooltip("How much damage is done from a bullet")]
    public int damageAmount;
    [Tooltip("How long do you have to wait, in seconds, between firing shots.")]
    public float shootDelay;

    [Space]

    [Header("")]
    [Tooltip("How many bullets are spawned from a single shot.  Pistol would have this set to 1, shotguns would use higher numbers.  Reguardless of the number, one bullet is always consumed.")]
    public int bulletsPerShot = 1;
    [Tooltip("How much the bullet(s) can spread in degrees.")]
    public float bulletSpread = 0f;

    [Space]

    [Tooltip("The item that should act as the ammo for this gun.")]
    public ItemData ammoItems;

    [Space]
    [Tooltip("If true, the right mouse button can be held down for continuous shooting.")]
    public bool rapidFire;
}
