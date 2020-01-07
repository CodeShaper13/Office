using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LootTable", menuName = "ZPanic/LootTable", order = 1)]
public class LootTable : ScriptableObject {

    [SerializeField]
    private LootTableEntry[] entires;

    /// <summary>
    /// Returns a random Item from the LootTable.  Null is returned if the table is empty.  This is not normally a desiered effect.
    /// </summary>
    public ItemData getRndItem() {
        int totalWeight = 0;
        foreach(LootTableEntry entry in this.entires) {
            totalWeight += entry.weight;
        }

        int resultNum = UnityEngine.Random.Range(0, totalWeight);

        int j = 0;
        foreach(LootTableEntry entry in this.entires) {
            j += entry.weight;
            if(resultNum <= j) {
                return entry.item;
            }
        }

        Debug.LogWarning("LootTable \"" + this.name + "\" is returning null.  Is it empty?");
        return null;
    }

    [Serializable]
    public struct LootTableEntry {
        
        public ItemData item;
        [Min(0)]
        public int weight;
        [Tooltip("Disabled entries are ignored.")]
        [SerializeField]
        public bool disabled;
    }
}
