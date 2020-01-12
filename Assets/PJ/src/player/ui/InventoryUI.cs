using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    [SerializeField]
    private Button itemsUiButton;
    [SerializeField]
    private Button craftingUiButton;

    [SerializeField]
    private TabBase itemTab;
    [SerializeField]
    private TabBase craftingTab;

    private TabBase currentTab;

    private void Awake() {
        // Disable the Tab GameObjects in case they got left enabled durring dev.
        this.itemTab.gameObject.SetActive(false);
        this.craftingTab.gameObject.SetActive(false);

        Player p = this.GetComponentInParent<PlayerUI>().getOwner();
        this.itemTab.initTab(p);
        this.craftingTab.initTab(p);

        this.callback_switchTab((int)EnumTab.INVENTORY);
    }

    /// <summary>
    /// Called when the Inventory UI is opened.
    /// </summary>
    public void open() {
    }

    /// <summary>
    /// Called when the Inventory UI is closed.
    /// </summary>
    public void close() {
        this.currentTab.onTabClose();
    }

    public void callback_switchTab(int newTab) {
        if(this.currentTab != null) { // False only on open
            this.currentTab.onTabClose();
            this.currentTab.gameObject.SetActive(false);
        }

        // Only switch if the target tab is different.
        TabBase newTabObj = (EnumTab)newTab == EnumTab.INVENTORY ? this.itemTab : this.craftingTab;
        if(newTabObj != this.currentTab) {
            this.currentTab = newTabObj;

            this.currentTab.gameObject.SetActive(true);
            this.currentTab.onTabOpen();
        }
    }
}