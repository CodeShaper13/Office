using UnityEngine;

public class Hotbar : MonoBehaviour {

    [SerializeField]
    private HotbarSquare[] hotbarSquares;

    private Camera orthoCamera;

    private void Awake() {
        this.orthoCamera = this.GetComponentInParent<PlayerUI>().getOrthoCamera();
    }

    public void updateHotbarHighlight(Player player) {
        for(int i = 0; i < 4; i++) {
            this.hotbarSquares[i].setSelected(i == player.hotbarIndex.get());
        }
    }

    public void drawHotbarItems(ContainerContents<IItemBase> container) {
        for(int i = 0; i < 4; i++) {
            IItemBase item = container.getItem(i);
            if(item != null) {
                Transform t = this.hotbarSquares[i].transform;
                RenderHelper.renderItemMesh(item, t.position, t.rotation, t.localScale, this.orthoCamera);
            }
        }
    }
}
