using UnityEngine;

public class ItemEdible : ItemBase<ItemDataEdible> {

    public override void onRightClick(Player player) {
        base.onRightClick(player);

        player.health.heal(this.data.healthRestored);
        this.onConsume();

        ItemManager.destroyHeldItem(player);

        IItemBase item = ItemManager.create<IItemBase>(this.data.leftoverItem);
        item.setInWorld(false, Vector3.zero, Quaternion.identity);
        player.inventory.addItem(item);
    }

    /// <summary> Called when the food item is consumed.  Child classes use this to implement extra effects when eaten.  Called before the leftover item is added in. </summary>
    public virtual void onConsume() { }
}
