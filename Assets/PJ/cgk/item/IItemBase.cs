using UnityEngine;

public interface IItemBase {

    Transform getTransform();

    ItemData getData();

    void setData(ItemData data);

    /// <summary>
    /// Returns true if the item in in the world and not in a container/used for rendering.
    /// </summary>
    bool isInWorld();

    /// <summary>
    /// Sets the item to be "in the world".  This will set the parent to be null.
    /// </summary>
    void setInWorld(bool inWorld, Vector3 position, Quaternion rotation);

    void hideItem();

    void showItem();
}
