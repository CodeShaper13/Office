using UnityEngine;

[CreateAssetMenu(fileName = "ItemEdible", menuName = "ZPanic/Items/ItemEdible", order = 1)]
public class ItemDataEdible : ItemData {

    [Space]

    [Tooltip("How much health is restored by eating the item.")]
    public int healthRestored;
    [Tooltip("The item that is left behind when the food item is consumed.  Can be null.")]
    public ItemData leftoverItem;
}
