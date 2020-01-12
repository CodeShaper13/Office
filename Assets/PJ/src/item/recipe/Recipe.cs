using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ZPanic/Recipe", order = 1)]
public class Recipe : ScriptableObject {

    [Header("The item(s) that go into the recipie.  There must be at least one.")]
    [SerializeField]
    private ItemData[] ingredients;

    [Header("The item that is made.  NOT optional.")]
    [SerializeField]
    private ItemData output;

    public ItemData[] getIngredients() {
        return this.ingredients;
    }

    public ItemData getResult() {
        return this.output;
    }

    public bool hasRequiredIngredients(ContainerContents<IItemBase> containerContents) {
        IItemBase[] items = containerContents.getRawItemArray();
        List<int> indicies = new List<int>();

        foreach(ItemData item in this.ingredients) {
            if(item != null) {

                for(int i = 0; i < items.Length; i++) {
                    if(items[i] != null && items[i].getData() == item) {
                        if(indicies.Contains(i)) {
                            continue;
                        } else {
                            indicies.Add(i);
                            break; // Found the item, go to the next ingredient
                        }
                    }
                }
            }
        }

        return indicies.Count == this.ingredients.Length;
    }
}
