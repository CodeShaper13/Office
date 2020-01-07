using UnityEngine;

public class ItemSpawnArea : Area {

    [SerializeField]
    [Tooltip("Areas that are disabled are ignored.")]
    private bool areaEnabled = true;
    [SerializeField]
    [Tooltip("The loot table to use.  If left empty, this area won't spawn items.")]
    private LootTable lootTable;
    [SerializeField]
    [Range(0, 100)]
    [Tooltip("The change that this Area will spawn an item as a percent.")]
    private float generationChance = 100;
    [Space]
    [Tooltip("If checked, an itme will generate when the scene is loaded.")]
    [SerializeField]
    private bool generateOnStart = true; 

    private void Start() {
        if(this.generateOnStart) {
            this.generateItem();
        }
    }

    public void generateItem() {
        if(this.areaEnabled == true && this.lootTable != null) {
            ItemData itemData = this.lootTable.getRndItem();
            if(itemData != null) {
                Quaternion rndRot = Quaternion.identity; // TODO
                IItemBase item = ItemManager.create<IItemBase>(itemData, this.getRndPoint(), rndRot);
            }
        }
    }
}