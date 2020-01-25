using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public abstract class CharacterNonPlayable : Character {

    [Header("Held Item")]
    [SerializeField]
    [Tooltip("The loot table used to generate the held item.")]
    private LootTable heldItemLootTable;
    [SerializeField]
    [Range(0f, 1f)]
    private float heldItemChance = 0.5f;

    protected Animator anim;

    public NavHelper navHelper;

    protected override void init() {
        base.init();

        this.navHelper = new NavHelper(this);
        this.anim = this.GetComponent<Animator>();
        this.inventory = new ContainerContents<IItemBase>(1, 1);

        if(this.heldItemLootTable != null && Random.Range(0f, 1f) <= this.heldItemChance) {
            ItemData itemData = this.heldItemLootTable.getRndItem();
            if(itemData != null) { // Null for empty loot tables.
                ItemManager.create<IItemBase>(itemData, this.inventory);
            }
        }

        // Characters are randomly picked to mirror their animation to make some variety.
        this.anim.SetBool("mirror", Random.Range(0, 2) == 1);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(this.transform.position, this.GetComponent<NavMeshAgent>().destination);
    }

    public override IItem getHeldItem() {
        return (IItem)this.inventory.getItem(0);
    }

    public override Vector2Int getContainerSize() {
        return new Vector2Int(1, 1);
    }
}
