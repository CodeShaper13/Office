using UnityEngine;
using UnityEngine.UI;

public class RecipeIngredientIcon : BtnBase {

    [SerializeField]
    private Color noColor = Color.white;
    [SerializeField]
    private Color haveItemColor = Color.green;
    [SerializeField]
    private Color noItemColor = Color.red;

    [SerializeField]
    private Image buttomMiddleImage;

    private ItemData item;

    private void Awake() {
        this.setColor(EnumColor.GRAY);
    }

    public override ItemData getItemToRender() {
        return this.item;
    }

    public void setItem(ItemData item) {
        this.item = item;
    }

    public void setColor(EnumColor color) {
        this.buttomMiddleImage.color = color == EnumColor.GRAY ? this.noColor : color == EnumColor.GREEN ? this.haveItemColor : this.noItemColor;
    }

    public enum EnumColor {
        GRAY,
        GREEN,
        RED,
    }
}
