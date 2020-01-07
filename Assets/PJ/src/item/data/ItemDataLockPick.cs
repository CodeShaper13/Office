using UnityEngine;

[CreateAssetMenu(fileName = "ItemLockPick", menuName = "ZPanic/Items/ItemLockPick", order = 1)]
public class ItemDataLockPick : ItemData {

    [Space]
    [Tooltip("How long in seconds it takes to pick a lock.")]
    public float pickTime = 1f;
    [Tooltip("How close the Player has to be to reach a lock.")]
    public float reach = 1f;
}
