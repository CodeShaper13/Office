public class ItemEdible : ItemBase<ItemDataEdible> {

    public override void onRightClick(Player player) {
        base.onRightClick(player);

        player.health.heal(this.data.healthRestored);

        ItemManager.destroyHeldItem(player);
    }
}
