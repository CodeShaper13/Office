using UnityEngine;
using System.Collections;
using InControl;

public class InputHelper {

    private InputDevice input;
    private int playerId;

    public InputHelper(int playerId) {
        this.playerId = playerId;
        this.input = InputManager.Devices[this.playerId];
    }

    //public InputControl a() {
    //    return this.input.
    //}
}
