using UnityEngine;
using UnityEngine.UI;

public class ButtonRecipe : BtnBase {

    [SerializeField]
    private Image outlineImage;

    private Recipe recipe;

    private void Awake() {
        this.setOutlined(false); // In case it got left on in the editor.
    }

    public override ItemData getItemToRender() {
        return this.recipe.getResult();
    }

    // Called after the object with this script is instantiated.  This sets references.
    public void setRecipie(Recipe recipe) {
        this.recipe = recipe;
    }

    /// <summary>
    /// Returns the recipe the button represents.
    /// </summary>
    public Recipe getRecipe() {
        return this.recipe;
    }

    public void setOutlined(bool hasOutline) {
        this.outlineImage.enabled = hasOutline;
    }
}
