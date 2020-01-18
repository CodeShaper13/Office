using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
[DisallowMultipleComponent]
[AddComponentMenu("ZPanic/GameMode/Safe House Trigger")]
public class SafeHouseTrigger : MonoBehaviour {

    [SerializeField]
    private Collider safeHouseCollider = null;

    private List<Player> playersInHouse;

    private void OnValidate() {
        if(this.safeHouseCollider == null) {
            this.safeHouseCollider = this.getCollider();
        }
        this.safeHouseCollider.isTrigger = true;
    }

    private void Awake() {
        if(this.safeHouseCollider == null) {
            this.safeHouseCollider = this.getCollider();
        }

        this.playersInHouse = new List<Player>();
    }

    private void OnTriggerEnter(Collider other) {
        Player p = other.GetComponent<Player>();
        if(p != null && !this.playersInHouse.Contains(p)) {
            this.playersInHouse.Add(p);
        }
    }

    private void OnTriggerExit(Collider other) {
        Player p = other.GetComponent<Player>();
        if(p != null) {
            this.playersInHouse.Remove(p);
        }
    }

    /// <summary>
    /// Returns true if all of the alive players are in the house.
    /// </summary>
    public bool allPlayersInHouse() {
        // Get the total of alive players
        int alive = 0;
        foreach(Player p in Main.singleton.playerManager.getAllPlayers<Player>()) {
            if(!p.health.isDead()) {
                alive += 1;
            }
        }
        return this.playersInHouse.Count == alive;
    }

    private Collider getCollider() {
        return this.GetComponent<Collider>();
    }
}
