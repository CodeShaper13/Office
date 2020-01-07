using UnityEngine;
using UnityEngine.UI;

public class ContainerHelper : MonoBehaviour {

    /// <summary>
    /// The item the mouse is holding when a container is open.
    /// </summary>
    private IItemBase heldItem;
    [SerializeField]
    private Text textHeldItemName;

    /// <summary>
    /// Returns the item held by the mouse.  May be null.
    /// </summary>
    public IItemBase getHeldStack() {
        return this.heldItem;
    }

    /// <summary>
    /// Sets the stack held by the mouse.  You can pass null.
    /// </summary>
    public void setHeldStack(IItemBase item) {
        if(item == null) {
            this.heldItem = null;
            this.textHeldItemName.text = string.Empty;
        }
        else {
            this.heldItem = item;
            this.textHeldItemName.text = item.getData().getUnlocalizedName();
        }
    }

    /// <summary>
    /// Draws the currently held item and info text
    /// </summary>
    public void renderHeldItem() {
        if(this.heldItem != null) {
            Vector3 mousePosition = References.list.hudCamera.ScreenToWorldPoint(Input.mousePosition);
            //Vector3 v = this.hudCamera.orthoCamera.ScreenToViewportPoint(Input.mousePosition);
            //this.heldTextName.transform.localPosition = new Vector3((v.x - 0.5f) * 800, (v.y - 0.5f) * 600);

            //Vector3 mousePosition = this.hudCamera.orthoCamera.ScreenToWorldPoint(Input.mousePosition);

            this.textHeldItemName.transform.localPosition = References.list.hudCamera.ScreenToViewportPoint(Input.mousePosition);

            RenderHelper.renderItemMesh(this.heldItem, mousePosition, Quaternion.identity, Vector3.one);
        }
    }
}
