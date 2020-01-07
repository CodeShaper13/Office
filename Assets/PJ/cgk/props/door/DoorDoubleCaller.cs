using UnityEngine;

public class DoorDoubleCaller : MonoBehaviour, IClickable<Player> {

    private DoorDouble doubleDoor;

    private void Awake() {
        this.doubleDoor = this.GetComponentInParent<DoorDouble>();
    }

    public bool onClick(Player player) {
        return this.doubleDoor.onClick(player);
    }
}
