using System.Linq;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour, IPlayer {

    public static Player singleton;

    private const float PICKUP_RANGE = 1.5f;

    [SerializeField]
    private float jumpPower = 20f;

    [SerializeField]
    private GameObject playerUIPrefab;

    private CharacterController cc;
    [HideInInspector]
    public Animator anim;
    private Camera cam;
    [HideInInspector]
    public PlayerUI playerUI;
    [SerializeField]
    private ContainerHelper containerHelper;
    [SerializeField]
    private Transform rightHandTransform;
    [SerializeField]
    private Transform leftHandTransform;

    /// <summary> The time in seconds since the player moved. </summary>
    private float timeSinceMove;
    /// <summary> The players health, 0-100. </summary>
    public Health health;
    public ScrollableInt hotbarIndex;
    public ContainerContents<IItem> inventory;
    private HeldItemDisplayer heldItemDisplayer;
    private FpsController fpsc;

    // Temp
    public ContainerItems startingInventory;


    private void Awake() {
        Player.singleton = this;

        this.playerUI = GameObject.Instantiate(this.playerUIPrefab).GetComponent<PlayerUI>();
        this.playerUI.setPlayer(this);

        this.cc = this.GetComponent<CharacterController>();
        this.anim = this.GetComponent<Animator>();
        this.cam = Camera.main;
        this.hotbarIndex = new ScrollableInt(0, 3);
        this.inventory = new ContainerContents<IItem>(4, 3);
        this.heldItemDisplayer = new HeldItemDisplayer(this, this.rightHandTransform, this.leftHandTransform);

        this.fpsc = this.GetComponent<FpsController>();

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

        // Update the UI
        this.playerUI.updateHealthCircle(this.health.getHealth());

        Vector3 motion = new Vector3();

        if(!this.health.isDead()) {
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
                        List<IItem> items = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IItem>().ToList<IItem>();

                        if(items.Count != 0) {
                            IItem closetItem = items.OrderBy(x => Vector2.Distance(this.transform.position, x.getTransform().position)).First<IItem>();
                            if(Vector3.Distance(this.transform.position, closetItem.getTransform().position) < Player.PICKUP_RANGE) {
                                // Pickup item.
                                this.pickupItem(closetItem);
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

            /*
            // Move the player.
            if(Input.GetKey(KeyCode.W)) {
                motion.z = 1;
            }
            if(Input.GetKey(KeyCode.S)) {
                motion.z = -1;
            }
            if(Input.GetKey(KeyCode.A)) {
                motion.x = -1f;
            }
            if(Input.GetKey(KeyCode.D)) {
                motion.x = 1;
            }
            */

            /*
            //Get the Screen positions of the object
            Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position + Vector3.up * 1.5f);
            //Get the Screen position of the mouse
            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //Get the angle between the points
            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
            //Ta Daaa
            // this.transform.rotation = Quaternion.Euler(new Vector3(0f, -angle - 90, 0));

            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(new Vector3(0f, -angle - 90, 0)), 720 * Time.deltaTime);
            */

            /*
            // Rotate the player in the direction they're walking.
            if(motion != Vector3.zero) {
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(motion), 720 * Time.deltaTime);
            }
            */


            /*
            // Move the player.  Doesn't the aniamtion clip do this, so now they go extra fast...?
            cc.Move(motion.setY(this.verticalVelocity) * Time.deltaTime);
            */

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

            /*
            // Update velocity/falling
            this.verticalVelocity += Physics.gravity.y * Time.deltaTime;
            */

            // Update the timeSinceMove field.
            if(motion == Vector3.zero) {
                this.timeSinceMove += Time.deltaTime;
            } else {
                this.timeSinceMove = 0f;
            }

            // Set the walking animation.
        //    this.anim.SetFloat("Speed_f", motion.setY(0) == Vector3.zero ? 0 : 1f);


            /*
            // Pick an idle animation if the player hasn't moved in a while.
            if((int)this.timeSinceMove % 10 == 0) {
                // Start of a 10 second period
                if(this.timeSinceMove > 5f) {
                    // Pick a random animation every 10 seconds,
                    // after the initial 10 seconds of basic idle.
                    this.anim.SetInteger("Animation_int", Random.Range(1, 3));
                }
            }
            else {
                this.anim.SetInteger("Animation_int", 0);
            }
            */
        }
    }

    private void OnDestroy() {
        
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void pickupItem(IItem item) {
        // print("Picking up item " + item.getData().getUnlocalizedName());

        item.setInWorld(false, Vector3.zero, Quaternion.identity); // TODO where should they be stored?

        this.inventory.addItem(item);
    }

    private void dropItem(IItem item, Vector3 pos) {
        // print("Dropping item " + item.getData().getUnlocalizedName());

        item.setInWorld(true, pos, Quaternion.identity);

        this.inventory.setItem(this.hotbarIndex.get(), null);
        this.heldItemDisplayer.heldItemLastFrame = null;
    }

    /// <summary>
    /// Returns the held item.  May be null.
    /// </summary>
    public IItem getHeldItem() {
        return this.inventory.getItem(this.hotbarIndex.get());
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

    public ContainerHelper getContainerHelper() {
        return this.containerHelper;
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

    public Camera getCamera() {
        return Camera.main; // TODO
    }
}
