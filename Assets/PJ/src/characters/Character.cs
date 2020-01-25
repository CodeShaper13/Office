using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Character : MonoBehaviour {

    [SerializeField]
    private GameObject bloodEffectPrefab;

    public Health health;

    public ContainerContents<IItemBase> inventory;

    private void Awake() {

    }

    private void Start() {
        this.health.subscribeToDamageEvent(onDamageCallback);

        Vector2Int inventorySize = this.getContainerSize();
        this.inventory = new ContainerContents<IItemBase>(inventorySize.x, inventorySize.y);

        this.init();
    }

    private void Update() {
        if(!Pause.isPaused()) {
            this.update();
        }
    }

    protected virtual void init() { }

    protected virtual void update() { }

    /// <summary>
    /// Returns the item currently being held by the character.  May be null.
    /// </summary>
    public abstract IItem getHeldItem();

    public abstract Vector2Int getContainerSize();

    protected void pickupItem(IItem item) {
        // It's possible the layer got set to enable collison with this world,
        // this happens to thrown items.  Reset it here.
        item.getTransform().gameObject.layer = Layers.ITEM;

        item.setInWorld(false, Vector3.zero, Quaternion.identity); // TODO where should they be stored?

        this.inventory.addItem(item);
    }

    protected void dropItem(int index, Vector3 pos) {
        IItemBase item = this.inventory.getItem(index);
        if(item != null) {
            item.setInWorld(true, pos, Quaternion.identity);
            this.inventory.setItem(index, null);


            if(this is Player) {
                ((Player)this).heldItemDisplayer.heldItemLastFrame = null;
            }
        }
    }

    /// <summary>
    /// Returns the position of the character's feet.
    /// </summary>
    public Vector3 getFootPos() {
        return this.transform.position;
    }

    private void onDamageCallback(int amount, RaycastHit? hit) {
        if(!Options.lowViolence) {
            if(this.bloodEffectPrefab != null) {
                if(hit != null) {
                    RaycastHit rayHit = (RaycastHit)hit;
                    GameObject spawnedDecal = GameObject.Instantiate(this.bloodEffectPrefab, rayHit.point, Quaternion.LookRotation(rayHit.normal));
                    spawnedDecal.transform.SetParent(rayHit.collider.transform);
                }
            }
        }

        if(this.health.isDead()) {
            // Make the ragdoll floppy
            Ragdoll doll = this.GetComponent<Ragdoll>();
            if(doll != null) {
                this.GetComponent<Ragdoll>().makeFloppy(true, true);
            }

            // Drop the held item
            if(this.getHeldItem() != null) {
                this.dropItem(0, this.getFootPos());
            }
        }
    }
}
