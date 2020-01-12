using UnityEngine;

[CreateAssetMenu(fileName = "ItemFlare", menuName = "ZPanic/Items/ItemFlare", order = 1)]
public class ItemDataFlare : ItemDataThrowable {

    [Space]

    [Min(0)]
    [Tooltip("")]
    public float litTime;
    [Min(0)]
    [Tooltip("How far away zombies are attracked to the flare.")]
    public float zombieAttractRange;
}
