using UnityEngine;

[CreateAssetMenu(fileName = "ItemLight", menuName = "ZPanic/Items/ItemLight", order = 1)]
public class ItemDataFlashlight : ItemData {

    [Space]
    [Min(0)]
    [Tooltip("What percent of the battery is consumed per second.")]
    public float batteryConsumeSpeed = 1f;
    [Space]
    [Tooltip("Item that should act as a battery for the flashlight.")]
    public ItemData batteryItem;
}
