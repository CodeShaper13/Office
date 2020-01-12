using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the item held by the mouse when a container is open.
/// </summary>
public class ContainerHeldItem : MonoBehaviour {

    [SerializeField]
    [Tooltip("The scale of the model while in hand.")]
    private float objectScale = 1f;
    [SerializeField]
    [Tooltip("The objects position on the z axis.")]
    private float zDistance = 0f;
    [SerializeField]
    private Text textHeldItemName;

    /// <summary> The item the mouse is holding when a container is open. </summary>
    private IItemBase heldItem;
    /// <summary> The camera rendering the mesh. </summary>
    private Camera orthoCamera;

    private void Start() {
        this.orthoCamera = this.GetComponent<PlayerUI>().getOrthoCamera();
    }

    /// <summary>
    /// Returns the item held by the mouse.  May be null.
    /// </summary>
    public IItemBase getHeldItem() {
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
            this.textHeldItemName.text = ((IItem)item).getItemName();
        }
    }

    /// <summary>
    /// Draws the currently held item and info text
    /// </summary>
    public void renderHeldItem() {
        if(this.heldItem != null) {
            Vector3 mousePosition = this.orthoCamera.ScreenToWorldPoint(Input.mousePosition);

            // Set text position (broken!)
            Vector3 v = this.orthoCamera.ScreenToViewportPoint(Input.mousePosition);
            this.textHeldItemName.transform.localPosition = new Vector3((v.x - 0.5f) * 800, (v.y - 0.5f) * 600);

            RenderHelper.renderItemMesh(this.heldItem, mousePosition + Vector3.forward * this.zDistance, Quaternion.identity, Vector3.one * this.objectScale);
        }
    }
}
