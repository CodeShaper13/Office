using UnityEngine;

public abstract class DoorBase : MonoBehaviour, IClickable<Player> {

    public const float TRIGGER_BLOAT_SIZE = 1f;

    private bool interactedWith;

    /// <summary> Field that saves in the door is locked.  Locked doors can not be opened. </summary>
    [Tooltip("Locked doors can not be opened.  They must be unlocked first.")]
    public bool isLocked;

    public bool isOpen;

    [Min(-1)]
    [Tooltip("The doors \"health\", or how many hits until it's broken.  -1 means it can't be broken.")]
    public float strength = -1;
    [Tooltip("The audio source to play when the door breaks.")]
    public AudioSource audioBreakingDoor;

    [HideInInspector]
    public BoxCollider[] boxColliders;

    private void Awake() {
        this.isOpen = this.detectIfOpen();

        // Create the trigger collider
        this.boxColliders = this.GetComponentsInChildren<BoxCollider>();
        this.createNavTrigger();
        
    }

    private void Update() {
        if(!Pause.isPaused() && this.interactedWith) {
            this.updateDoor();
        }
    }

    private void OnTriggerEnter(Collider other) {
        ZombieBase monster = other.GetComponentInParent<ZombieBase>();
        if(!this.isOpen && monster != null && monster.navHelper.door == null && this.strength > 0) {
            monster.navHelper.setDoor(this);
        }
    }

    public virtual bool onClick(Player player) {
        if(!this.isLocked) {
            this.isOpen = !this.isOpen;
            this.interactedWith = true;
            return true;
        } else {
            return false;
        }
    }

    public virtual void destroyDoor() {
        this.GetComponent<MeshRenderer>().enabled = false;
        foreach(BoxCollider bc in this.boxColliders) {
            bc.enabled = false;
        }
        GameObject.Destroy(this.gameObject, 2f);
    }

    public virtual void createNavTrigger() {
        BoxCollider trigger = this.gameObject.AddComponent<BoxCollider>();
        trigger.center = boxColliders[0].center;
        trigger.size = new Vector3(boxColliders[0].size.x, boxColliders[0].size.y, boxColliders[0].size.z + TRIGGER_BLOAT_SIZE);
        trigger.isTrigger = true;
    }

    protected abstract void updateDoor();

    public abstract bool detectIfOpen();

    public abstract void setAsOpen();

    public abstract void setAsClosed();
}
