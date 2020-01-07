using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "PlayerState", menuName = "ZPanic/Stats/PlayerState", order = 1)]
public class PlayerStats : ScriptableObject {

    public int maxHealth;
    public float itemPickupRange;
    public float jumpPower;
}
