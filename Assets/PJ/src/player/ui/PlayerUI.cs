using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour, IPlayerUI {

    [SerializeField]
    private HealthBarUI healthBarUI;
    [SerializeField]
    private Hotbar hotbar;
    [SerializeField]
    private Text bulletCountText;
    [SerializeField]
    private InventoryUI inventoryUI;
    [SerializeField]
    private Camera orthoCamera;
    [SerializeField]
    private Image crosshairs;

    [SerializeField]
    public ContainerHeldItem heldItem;

    /// <summary> The player who owns this UI. </summary>
    private Player owner;

    private void Update() {
        this.healthBarUI.updateHealthBar(this.owner.health);

        // Make the crosshairs visible based on the held item.
        //this.crosshairs.enabled = t


        this.hotbar.updateHotbarHighlight(this.owner);
        this.hotbar.drawHotbarItems(this.owner.inventory);

        // Draw the item the mouse is holding.
        this.heldItem.renderHeldItem();
    }

    /// <summary>
    /// Returns true if the Inventory is open.
    /// </summary>
    public bool isInventoryOpen() {
        return this.inventoryUI.gameObject.activeInHierarchy;
    }

    public void openInventory() {
        this.inventoryUI.gameObject.SetActive(true);
        this.inventoryUI.open();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // TODO confine for release.
    }

    public void closeInventory() {
        this.inventoryUI.gameObject.SetActive(false);
        this.inventoryUI.close();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void setExtraText(string text) {
        this.bulletCountText.text = text == null ? string.Empty : text;
    }

    public void setPlayer(IPlayer owner) {
        this.owner = (Player)owner;
        this.owner.playerUI = this;
    }

    public Player getOwner() {
        return this.owner;
    }

    public Camera getOrthoCamera() {
        return this.orthoCamera;
    }
}
