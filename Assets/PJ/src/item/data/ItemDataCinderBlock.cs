using UnityEngine;

[CreateAssetMenu(fileName = "ItemCinderBlock", menuName = "ZPanic/Items/ItemCinderBlock", order = 1)]
public class ItemDataCinderBlock : ItemDataThrowable {

    [Space]

    [Tooltip("The damage when the item hit's an entity.")]
    public int damage;
}
