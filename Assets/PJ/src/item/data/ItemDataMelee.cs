using UnityEngine;

[CreateAssetMenu(fileName = "ItemMelee", menuName = "ZPanic/Items/ItemMelee", order = 1)]
public class ItemDataMelee : ItemData {

    [Space]

    [Min(0)]
    [Tooltip("How much damage the item deals.")]
    public int damage = 0;
    [Min(0)]
    [Tooltip("The range, in units, that the weapon reaches.")]
    public float range = 1f;
    [Min(0)]
    [Tooltip("How often, in seocnds, you can attack with this item.")]
    public float attackRate = 0.5f;
}
