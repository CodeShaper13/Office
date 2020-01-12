using UnityEngine;

public abstract class ItemBase<T> : MonoBehaviour, IItem where T : ItemData {

    protected T data;

    private bool inWorld;
    private Vector3 worldScale;

    private void Awake() {
        this.worldScale = this.transform.localScale;

        this.initializeItem();
    }

    private void Update() {
        if(!Pause.isPaused()) {
            this.updateItemInWorld();
        }
    }

    public Transform getTransform() {
        return this.transform;
    }

    public ItemData getData() {
        return data;
    }

    public bool isInWorld() {
        return this.inWorld;
    }

    public virtual void setInWorld(bool inWorld, Vector3 position, Quaternion rotation) {
        this.inWorld = inWorld;
        
        if(this.inWorld) {
            this.showItem();

            // Put the object the world and free it from it's parent (likely a "Hand" transform or container).
            this.transform.parent = null;
            this.transform.position = position;
            this.transform.rotation = rotation;
            this.transform.localScale = this.worldScale;
        } else {
            this.hideItem();
        }
    }

    public void setData(ItemData data) {
        this.data = (T)data;
    }

    /// <summary> Called when the Item's Gameobject is first instantiated. </summary>
    public virtual void initializeItem() { }

    public virtual string getItemName() {
        return this.data.getUnlocalizedName();
    }

    public virtual void hideItem() {
        this.GetComponent<MeshRenderer>().enabled = false;
    }

    public virtual void showItem() {
        this.GetComponent<MeshRenderer>().enabled = true;
    }

    public virtual void updateItemInWorld() { }

    public virtual void updateItemInHand(Player player) { }

    public virtual void onLeftClick(Player player) { }

    public virtual void onRightClick(Player player) { }

    public virtual void onReloadPress(Player player) { }

    public virtual EnumInputBlock getInputBlock(Player player) {
        return EnumInputBlock.NONE;
    }

    public virtual void animatorUpdate(Player player, Animator anim) { }

    public virtual string getExtraText(Player player) {
        return string.Empty;
    }

    public virtual bool canPickUpItem(Player player) {
        return true;
    }
}
