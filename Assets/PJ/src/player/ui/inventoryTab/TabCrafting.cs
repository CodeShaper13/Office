using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TabCrafting : TabBase {

    [SerializeField]
    private GameObject slotPrefab;

    [SerializeField]
    private RectTransform recipeRect;
    [SerializeField]
    private Button craftButton;
    [SerializeField]
    private RecipeIngredientIcon[] ingredientIcons;

    private List<ButtonRecipe> recipeButtons;
    /// <summary> The currenlty selected recipe.  May be null. </summary>
    private ButtonRecipe selectedRecipe;
    /// <summary> An array of all of the recipies. </summary>
    private Recipe[] allRecipies;

    private void Awake() {
        // Load the recipies from the resources folder.
        this.allRecipies = Resources.LoadAll<Recipe>("recipes");

        this.recipeButtons = new List<ButtonRecipe>();

        // Generate all of the slots.
        const int btnsPerRow = 6;
        const float sidePad = 16;

        float x = sidePad;
        float y = -sidePad;

        float btnSize = this.slotPrefab.GetComponent<RectTransform>().sizeDelta.y;

        foreach(Recipe recipe in this.allRecipies) {
            if(recipe != null) {
                ButtonRecipe recipeBtn = GameObject.Instantiate(this.slotPrefab, this.recipeRect).GetComponent<ButtonRecipe>();
                recipeBtn.transform.localPosition = new Vector3(x + (btnSize / 2), y - (btnSize / 2), 0);
                recipeBtn.setRecipie(recipe);
                this.recipeButtons.Add(recipeBtn);

                recipeBtn.GetComponent<Button>().onClick.AddListener(delegate {
                    if(this.selectedRecipe != null) {
                        this.selectedRecipe.setOutlined(false);
                    }
                    recipeBtn.setOutlined(true);
                    this.setSelectedRecipe(recipeBtn);
                });

                x += btnSize + sidePad;

                if(x > 500) {
                    x = sidePad;
                    y -= btnSize + sidePad;
                }
            }
        }

        this.updateCraftButtonInteractable();
    }

    public override void onTabOpen() {
        base.onTabOpen();

        // Color the recipe buttons green if the player can make that item, false if they can't
        foreach(ButtonRecipe btn in this.recipeButtons) {
            Recipe r = btn.getRecipe();
            foreach(ItemData item in r.getIngredients()) {
                // TODO
            }
        }
    }

    public override void onUpdate() {
        base.onUpdate();

        // Render all of the items.
        foreach(ButtonRecipe btn in this.recipeButtons) {
            btn.renderItem();
        }

        foreach(RecipeIngredientIcon icon in this.ingredientIcons) {
            icon.renderItem();
        }
    }

    /// <summary>
    /// Sets the currently selected recipie.
    /// </summary>
    public void setSelectedRecipe(ButtonRecipe recipe) {
        this.selectedRecipe = recipe;

        ItemData[] ingredients;
        if(recipe == null) {
            ingredients = new ItemData[0];
        } else {
            ingredients = recipe.getRecipe().getIngredients();
        }
        for(int i = 0; i < this.ingredientIcons.Length; i++) {
            RecipeIngredientIcon icon = this.ingredientIcons[i];

            if(i <= ingredients.Length - 1) {
                icon.setItem(ingredients[i]);
                icon.setColor(this.getPlayer().inventory.containsItem(ingredients[i], out int j) ? RecipeIngredientIcon.EnumColor.GREEN : RecipeIngredientIcon.EnumColor.RED);
            }
            else {
                icon.setItem(null);
                icon.setColor(RecipeIngredientIcon.EnumColor.GRAY);
            }
        }

        this.updateCraftButtonInteractable();
    }

    /// <summary>
    /// Called when the craft button is clicked.
    /// </summary>
    public void callback_craftButtonClick() {
        Recipe r = this.selectedRecipe.getRecipe();
        ContainerContents<IItemBase> inventory = this.getPlayer().inventory;

        // Remove the items from the players inventory.
        foreach(ItemData item in r.getIngredients()) {
            int i;
            inventory.containsItem(item, out i);
            if(i != -1) {
                ItemManager.destroyItem(this.getPlayer(), i);
            }
        }

        // Add the new item to the players inventory.
        if(r.getResult() != null) {
            IItemBase item = ItemManager.create<IItemBase>(r.getResult());
            item.setInWorld(false, Vector3.zero, Quaternion.identity);
            inventory.addItem(item);
        }

        // Set the selected recipe to be null.
        this.selectedRecipe.setOutlined(false);
        this.setSelectedRecipe(null);
    }

    /// <summary>
    /// Enables or disables the craft button depending on if a recipie is selected.
    /// </summary>
    private void updateCraftButtonInteractable() {
        if(this.selectedRecipe == null) {
            this.craftButton.interactable = false;
        } else {
            Recipe r = this.selectedRecipe.getRecipe();
            this.craftButton.interactable = r.hasRequiredIngredients(this.getPlayer().inventory);
        }
    }
}
