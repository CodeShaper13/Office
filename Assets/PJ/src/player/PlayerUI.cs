using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour, IPlayerUI {

    [SerializeField]
    private Color healthGreen = Color.white;
    [SerializeField]
    private Color healthOrange = Color.white;
    [SerializeField]
    private Color healthRed = Color.white;

    [SerializeField]
    private Text bulletCountText;
    //[SerializeField]
    //private Image healthImage;
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Image healthSliderImage;
    [SerializeField]
    private HotbarSquare[] hotbarSquares;
    [SerializeField]
    private Camera hudCamera;

    /// <summary> The player who owns this UI. </summary>
    private Player player;

    private void Start() {
        this.GetComponent<Canvas>().worldCamera = HudCamera.getCamera();
        //this.GetComponentInParent<Canvas>().planeDistance = -5f;
    }

    private void Update() {
        // Update the selected hotbar square color.
        for(int i = 0; i < 4; i++) {
            this.hotbarSquares[i].setSelected(i == this.player.hotbarIndex.get());
        }

        // Draw the items in the Hotbar.
        for(int i = 0; i < 4; i++) {
            IItem item = this.player.inventory.getItem(i);
            if(item != null) {
                Transform t = this.hotbarSquares[i].transform;
                RenderHelper.renderItemMesh(this.player.inventory.getItem(i), t.position, t.rotation, t.localScale);
            }
        }

        // Draw the item the mouse is holding.
        this.player.getContainerHelper().renderHeldItem();
    }

    public void setExtraText(string text) {
        this.bulletCountText.text = text == null ? string.Empty : text;
    }

    public void updateHealthCircle(int hp) {
        Color c;
        if(hp > 50) {
            c = this.healthGreen;
        } else if(hp > 25) {
            c = this.healthOrange;
        } else {
            c = this.healthRed;
        }

        this.healthSliderImage.color = c;
        this.healthSlider.value = hp / 100f;

        /*
        this.healthImage.color = c;
        this.healthImage.fillAmount = hp / 100f;
        */
    }

    public void setPlayer(IPlayer owner) {
        this.player = (Player)owner;
        this.player.playerUI = this;
    }
}
