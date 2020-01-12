public class ItemEdible : ItemBase<ItemDataEdible> {

    public override void onRightClick(Player player) {
        base.onRightClick(player);

        player.health.heal(this.data.healthRestored);

        int i = player.hotbarIndex.get();
        ItemManager.destroy(player.inventory.getItem(i));
        player.inventory.setItem(i, null);
    }
}
