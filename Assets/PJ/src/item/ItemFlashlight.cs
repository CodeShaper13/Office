using UnityEngine;

public class ItemFlashlight : ItemBase<ItemDataFlashlight> {

    [SerializeField]
    private Light lightSource;
    [SerializeField]
    private Transform lens;

    private bool isOn = false;
    private float batteryLeft;

    public override void setInWorld(bool inWorld, Vector3 position, Quaternion rotation) {
        base.setInWorld(inWorld, position, rotation);

        if(inWorld) {
            this.turnOff();
        }
    }

    public override void showItem() {
        base.showItem();

        this.lens.gameObject.SetActive(true);
    }

    public override void hideItem() {
        base.hideItem();

        this.lens.gameObject.SetActive(false);
    }

    public override void updateItemInHand(Player player) {
        base.updateItemInHand(player);

        if(this.isOn) {
            this.batteryLeft -= Time.deltaTime;

            if(this.batteryLeft <= 0) {
                this.turnOff();
            }
        }
    }

    public override void onReloadPress(Player player) {
        base.onReloadPress(player);

        int index;
        if(player.inventory.containsItem(this.data.batteryItem, out index)) {
            ItemManager.destroy(player.inventory.getItem(index));
            player.inventory.setItem(index, null);

            this.batteryLeft = 100;
        }
    }

    public override void onRightClick(Player player) {
        base.onRightClick(player);

        if(this.isOn) {
            this.turnOff();
        } else {
            if(this.batteryLeft > 0) {
                this.turnOn();
            }
        }
    }

    public override string getExtraText(Player player) {
        int i = (int)(Mathf.Ceil(this.batteryLeft / 10f) * 10f);
        return i + "%";
    }

    private void turnOn() {
        this.isOn = true;
        this.lightSource.enabled = true;
        this.lens.gameObject.SetActive(true);
    }

    private void turnOff() {
        this.isOn = false;
        this.lightSource.enabled = false;
        this.lens.gameObject.SetActive(false);
    }
}
