using UnityEngine;
using System.Collections.Generic;

public class World : MonoBehaviour {

    public List<IItem> allItems;

    private void Awake() {
        this.allItems = new List<IItem>();
    }
}
