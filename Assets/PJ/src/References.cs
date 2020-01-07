using UnityEngine;

public class References : MonoBehaviour {

    public static References list;

    [Header("The Camera that draws items.")]
    public Camera hudCamera;

    [Header("Materials")]
    public Material itemMaterialLit_0;
    public Material itemMaterialLit_1;
    public Material itemMaterialUnlit_0;
    public Material itemMaterialUnlit_1;

    public ItemData[] items = new ItemData[ItemData.MAX_ID];

    public static void bootstrap() {
        References.list = GameObject.FindObjectOfType<References>();

        // Init all the items, and make sure none share IDs.
        foreach(ItemData item in References.list.items) {
            if(item != null) {
                item.init();
            }
        }
    }
}
