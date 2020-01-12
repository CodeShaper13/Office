using UnityEngine;

[CreateAssetMenu(fileName = "ZombieStats", menuName = "ZPanic/Stats/ZombieStats", order = 1)]
public class ZombieStats : ScriptableObject {

    [Min(0)]
    public float attackRate;
    [Min(0)]
    public int attackDamageAmount;
    [Min(0)]
    public float attackReach;
}
