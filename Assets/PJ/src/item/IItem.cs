using UnityEngine;

public interface IItem : IItemBase {

    /// <summary>
    /// Returns the Item's unlocalized name.
    /// </summary>
    string getItemName();

    /// <summary>
    /// Called every frame, even when the item is in a container.
    /// Use isInworld() to check if the item is in an inventory.
    /// </summary>
    void updateItemInWorld();

    /// <summary>
    /// Called every frame that the item is in a Player's hand.
    /// </summary>
    void updateItemInHand(Player player);

    void onLeftClick(Player player);

    void onRightClick(Player player);

    void onRightClickHold(Player player);

    void onReloadPress(Player player);

    EnumInputBlock getInputBlock(Player player);

    void animatorUpdate(Player player, Animator anim);

    string getExtraText(Player player);

    bool canPickUpItem(Player player);
}
