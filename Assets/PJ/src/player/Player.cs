using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : Character, IPlayer {

    private const float PICKUP_RANGE = 1.5f;

    [SerializeField]
    private GameObject playerUIPrefab;

    private CharacterController cc;
    private Animator anim;
    private FpsControl fpsc;
    public HeldItemDisplayer heldItemDisplayer;
    /// <summary> The time in seconds since the player moved. </summary>
    private float timeSinceMove;

    [HideInInspector]
    public PlayerUI playerUI;
    [SerializeField]
    private Transform rightHandTransform;
    [SerializeField]
    private Transform leftHandTransform;
    [SerializeField]
    private Camera firstPersonCamera;

    public ScrollableInt hotbarIndex;
    public ContainerContents<IItemBase> inventory;


    // Temp
    public ContainerItems startingInventory;


    private void Awake() {
        this.playerUI = GameObject.Instantiate(this.playerUIPrefab).GetComponent<PlayerUI>();
        this.playerUI.setPlayer(this);

        this.cc = this.GetComponent<CharacterController>();
        this.anim = this.GetComponent<Animator>();
        this.hotbarIndex = new ScrollableInt(0, 3);
        this.inventory = new ContainerContents<IItemBase>(4, 3);
        this.heldItemDisplayer = new HeldItemDisplayer(this, this.rightHandTransform, this.leftHandTransform);

        this.fpsc = this.GetComponent<FpsControl>();

        // Temp
        this.startingInventory.fillContainer(this.inventory);

        // Let animations move the player.
        this.anim.SetBool("Static_b", false);
    }

    private void Start() {
        this.health.setHealth(100);
        this.health.subscribeToDamageEvent(this.onDamageEvent);
    }

    private void Update() {
        if(Pause.isPaused()) {
            return;
        }

        // Open the pause menu
        if(Input.GetKeyDown(KeyCode.Escape) && !this.playerUI.isInventoryOpen() && !Pause.isPaused()) {
            Main.singleton.uiManager.openUI(Main.singleton.uiManager.pauseUi);
        }

        if(!this.health.isDead()) {

            // Update the held item model in the Players hand.
            this.heldItemDisplayer.updateHeldItem(this.getHeldItem());

            // Update the extra text.
            this.playerUI.setExtraText(this.getHeldItem() == null ? string.Empty : this.getHeldItem().getExtraText(this));

            // Update the animator based on the held item.
            IItem itemInHand = this.getHeldItem();
            if(itemInHand != null) {
                itemInHand.animatorUpdate(this, this.anim);
            }
            this.anim.SetInteger("WeaponType_int", itemInHand == null ? 0 : (int)itemInHand.getData().getIdleAnimation());

            if(this.playerUI.isInventoryOpen()) {
                if(Input.GetKeyDown(KeyCode.Escape)) {
                    this.playerUI.closeInventory();
                }
            } else {
                if(Input.GetKeyDown(KeyCode.Tab)) {
                    this.playerUI.openInventory();
                    return; // Dont procees player input.
                }

                this.anim.SetBool("Jump_b", false);

                // Update the held item.
                if(this.getHeldItem() != null) {
                    this.getHeldItem().updateItemInHand(this);
                }

                EnumInputBlock inputBlockMask = this.getHeldItem() == null ? EnumInputBlock.NONE : this.getHeldItem().getInputBlock(this);
                bool blockMove = (inputBlockMask & EnumInputBlock.MOVE) == EnumInputBlock.MOVE;
                bool blockHeldChange = (inputBlockMask & EnumInputBlock.CHANGE_HELD) == EnumInputBlock.CHANGE_HELD;
                bool blockLook = (inputBlockMask & EnumInputBlock.LOOK) == EnumInputBlock.LOOK;

                // Move Player.
                this.fpsc.updateController(blockMove, blockLook);

                if(!blockHeldChange) {
                    // Scroll through the hotbar.
                    this.hotbarIndex.scroll(((int)Input.mouseScrollDelta.y) * -1);

                    // Drop held item if Q is pressed.
                    if(Input.GetKeyDown(KeyCode.Q)) {
                        if(this.getHeldItem() != null) {
                            const float FORWARD_CHECK_DIST = 2f;
                            const float DOWN_CHECK_DIST = 4f;

                            Vector3 playerHandHeight = this.transform.position + Vector3.up * 2f;
                            if(!Physics.Raycast(playerHandHeight, this.transform.forward, FORWARD_CHECK_DIST)) {
                                // Nothing directly in front of the player.
                                RaycastHit hit;
                                if(Physics.Raycast(playerHandHeight + this.transform.forward * FORWARD_CHECK_DIST, Vector3.down, out hit, DOWN_CHECK_DIST)) {
                                    // A floor is under them.
                                    this.dropItem(this.getHeldItem(), hit.point);
                                }
                            }
                        }
                    }

                    // Try to pick up an item if E is pressed.
                    if(Input.GetKeyDown(KeyCode.E)) {
                        if(!this.inventory.isFull()) {
                            const float pickupRange = 5f;
                            RaycastHit hit;
                            if(Physics.Raycast(this.getCameraRay(), out hit, pickupRange, (Layers.ITEMS_MASK | Layers.DEFAULT_MASK))) {

                                // If it's an item, try and pick it up.
                                IItem item = hit.transform.GetComponent<IItem>();
                                if(item != null) { // Safety check.
                                    this.pickupItem(item);
                                }

                                // If it implements IClickible, interact with it
                                IClickable<Player> clickable = hit.transform.GetComponentInParent<IClickable<Player>>();
                                if(clickable != null) {
                                    clickable.onClick(this);
                                }
                            }
                        }
                    }

                    if(this.getHeldItem() != null) {
                        // Let the held item respond to buttons being pressed. 
                        if(Input.GetKeyDown(KeyCode.R)) {
                            this.getHeldItem().onReloadPress(this);
                        }
                        if(Input.GetMouseButtonDown(0)) {
                            this.getHeldItem().onLeftClick(this);
                        }
                        if(Input.GetMouseButtonDown(1)) {
                            this.getHeldItem().onRightClick(this);
                        }
                    }
                }
            }
        }
    }

    private void pickupItem(IItem item) {
//        print("Picking up item " + item.getData().getUnlocalizedName());

        // It's possible the layer got set to enable collison with this world,
        // this happens to thrown items.  Reset it here.
        item.getTransform().gameObject.layer = Layers.ITEM;

        item.setInWorld(false, Vector3.zero, Quaternion.identity); // TODO where should they be stored?

        this.inventory.addItem(item);
    }

    private void dropItem(IItem item, Vector3 pos) {
//        print("Dropping item " + item.getData().getUnlocalizedName());

        item.setInWorld(true, pos, Quaternion.identity);

        this.inventory.setItem(this.hotbarIndex.get(), null);
        this.heldItemDisplayer.heldItemLastFrame = null;
    }

    /// <summary>
    /// Returns the held item.  May be null.
    /// </summary>
    public IItem getHeldItem() {
        return (IItem)this.inventory.getItem(this.hotbarIndex.get());
    }

    /// <summary>
    /// Returns true if the player is on the ground.  Use instead of CharacterController.isGrounded().
    /// </summary>
    public bool isOnGround() {
        RaycastHit hit;
        return Physics.Raycast(this.getFootPos() + Vector3.up, Vector3.down, out hit, 1.25f);
    }

    /// <summary>
    /// Returns a Ray coming out form the camera.  Use then when raycasting.
    /// </summary>
    public Ray getCameraRay() {
        return new Ray(this.getCamera().transform.position, this.getCamera().transform.forward);
    }

    /// <summary>
    /// Casts a ray coming out from the Player's camera.
    /// </summary>
    public bool raycast(out RaycastHit hit, float maxDistance) {
        return Physics.Raycast(this.getCameraRay(), out hit, maxDistance);
    }

    /// <summary>
    /// Returns the position of the players feet.
    /// </summary>
    public Vector3 getFootPos() {
        return this.transform.position;
    }

    /// <summary>
    /// Damages the Player.  Returns true if they are now dead.
    /// </summary>
    public void onDamageEvent(int amount) {
        if(this.health.isDead()) {
            this.setDead(true); // TODO bool param
        }
    }

    public void setDead(bool frontDeath) {
        this.anim.SetBool("Death_b", true);
        this.anim.SetInteger("DeathType_int", frontDeath ? 2 : 1);
    }

    public Transform getTransform() {
        return this.transform;
    }

    /// <summary>
    /// Returns the first person camera used by the player.
    /// </summary>
    public Camera getCamera() {
        return this.firstPersonCamera;
    }
}
