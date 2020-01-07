using UnityEngine;
using System.Collections;

public static class ItemBuilder {

    /*
    public static ItemGoBase<ItemData> instantiate(ItemData data) {
        return ItemBuilder.instantiate(data, Vector3.zero, Quaternion.identity);
    }

    public static ItemGoBase<ItemData> instantiate(ItemData data, Vector3 position, Quaternion rotation) {
        GameObject prefab = data.getPrefab();
        if(prefab != null) {
            GameObject obj = GameObject.Instantiate(prefab, position, rotation);
            ItemGoBase<ItemData> itemGO = obj.GetComponent<ItemGoBasic>();
            return itemGO;
        }
        return null;
    }
    */
}
