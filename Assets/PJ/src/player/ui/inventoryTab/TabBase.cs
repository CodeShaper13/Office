using UnityEngine;
using UnityEngine.UI;

public abstract class TabBase : MonoBehaviour {

    [SerializeField]
    private Button tabButton;

    private Player player;

    public void initTab(Player player) {
        this.player = player; // this.GetComponentInParent<PlayerUI>().getOwner();
    }

    private void Update() {
        this.onUpdate();
    }

    public virtual void onTabOpen() {
        this.tabButton.transform.SetSiblingIndex(0);
    }

    public virtual void onTabClose() {
        //this.tabButton.transform.SetSiblingIndex(0);
    }
    
    public virtual void onUpdate() { }

    public Player getPlayer() {
        return this.player;
    }
}
