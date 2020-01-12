using UnityEngine;

[CreateAssetMenu(fileName = "ItemSmokeGrenade", menuName = "ZPanic/Items/ItemSmokeGrenade", order = 1)]
public class ItemDataSmokeGrenade : ItemDataThrowable {

    [Space]

    [Min(0)]
    [Tooltip("How long smoke lasts.")]
    public float smokeTime;
    [Min(0)]
    [Tooltip("The radius of the smoke cloud")]
    public float smokeRadius;
}
