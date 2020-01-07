using UnityEngine;

[CreateAssetMenu(fileName = "Recipie", menuName = "ZPanic/Recipie", order = 1)]
public class Recipie : ScriptableObject {

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
}
