using UnityEngine;

[CreateAssetMenu(fileName = "ContainerContents", menuName = "ZPanic/ContainerContents", order = 1)]
public class ContainerItems : ScriptableObject {

    [SerializeField]
    private ItemData[] items;

    public void fillContainer(ContainerContents<IItemBase> container) {
        foreach(ItemData itemData in this.items) {
            if(container.isFull()) {
                break;
            }

            if(itemData == null) {
                continue;
            }

            ItemManager.create<IItemBase>(itemData, container);
        }
    }
}
