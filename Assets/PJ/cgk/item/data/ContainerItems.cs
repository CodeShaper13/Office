using UnityEngine;

[CreateAssetMenu(fileName = "ContainerContents", menuName = "ZPanic/ContainerContents", order = 1)]
public class ContainerItems : ScriptableObject {

    [SerializeField]
    private ItemData[] items;

    public void fillContainer<T>(ContainerContents<T> container) where T : IItemBase {
        foreach(ItemData itemData in this.items) {
            if(container.isFull()) {
                break;
            }
            T item = ItemManager.create<T>(itemData);
            item.setInWorld(false, Vector3.zero, Quaternion.identity); // TODO where should they be stored?
            container.addItem(item);
        }
    }
}
