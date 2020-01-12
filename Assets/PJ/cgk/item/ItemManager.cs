using UnityEngine;

public static class ItemManager {

    /// <summary>
    /// Creates an item prefab from the passed ItemData.
    /// </summary>
    public static T create<T>(ItemData item) where T : IItemBase {
        return ItemManager.create<T>(item, Vector3.zero, Quaternion.identity);
    }

    /// <summary>
    /// Creates an item prefab from the passed ItemData.
    /// </summary>
    public static T create<T>(ItemData item, Vector3 position, Quaternion rotation) where T : IItemBase {
        GameObject prefab = item.getPrefab();
        if(prefab == null) {
            Debug.LogWarning("Tried to instantiate an Item that doesn't have a prefab set!  Item name = " + item.getUnlocalizedName());
        }
        GameObject obj = GameObject.Instantiate(prefab);
        T iItem = obj.GetComponent<T>();
        if(iItem == null) {
            Debug.LogWarning("Tried to instanties an Item that doesn't have a Item Component on the Prefab!");
        }
        iItem.setData(item);
        iItem.setInWorld(true, position, rotation);

        return iItem;
    }

    /// <summary>
    /// Destroys the passed Item.
    /// </summary>
    public static void destroy(IItemBase item) {
        GameObject.Destroy(item.getTransform().gameObject);
    }
}
