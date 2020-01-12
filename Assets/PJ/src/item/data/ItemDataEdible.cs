using UnityEngine;

[CreateAssetMenu(fileName = "ItemEdible", menuName = "ZPanic/Items/ItemEdible", order = 1)]
public class ItemDataEdible : ItemData {

    [Space]

    [Tooltip("How much health is restored by eating the item.")]
    public int healthRestored;
}
