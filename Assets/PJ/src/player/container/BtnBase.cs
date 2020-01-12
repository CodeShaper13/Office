using UnityEngine;

public abstract class BtnBase : MonoBehaviour {

    public void renderItem() {
        ItemData item = this.getItemToRender();
        if(item != null) {
            RenderHelper.renderItemMesh(item, this.transform.position, Quaternion.identity, this.transform.localScale);
        }
    }

    public abstract ItemData getItemToRender();
}
