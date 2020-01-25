using UnityEngine;
using System;

[AddComponentMenu("ZPanic/GameMode/Game Mode Call For Help")]
public class GameModeCallForHelp : GameModeBase {

    [Tooltip("The point that the vehicle spawns at.")]
    public Transform vehiclePoint;
    [Tooltip("The time until help arrives after calling them in seconds")]
    public float timeUntilHelp;

    private EnumState state;
    /// <summary> How much time has passed since making the call. </summary>
    private float timeElapsed;
    private ItemRadio radioItem;

    public override void initGameMode(Director director) {
        base.initGameMode(director);

        if(this.vehiclePoint == null) {
            throw new Exception("vehiclePoint can not be empty!");
        }
    }

    public override void updateGameMode() {
        base.updateGameMode();

        if(this.state == EnumState.WAIT) {
            this.timeElapsed += Time.deltaTime;
            
            if(this.timeElapsed >= this.timeUntilHelp) {
                this.state = EnumState.GET_IN_VEHICLE;

                this.playClipOnRadio(ItemRadio.EnumClip.ARRRIVED);

                // TODO spawn vehicle.
            }
        }
    }

    public override string[] getObjectiveText() {
        string s = "???";
        switch(this.state) {
            case EnumState.FIND_RADIO: s = "gamemode.callforhelp.objectiveLocateRadio"; break;
            case EnumState.WAIT: s = "gamemode.callforhelp.objectiveSurvive"; break;
            case EnumState.GET_IN_VEHICLE: s = "gamemode.callforhelp.objectiveGetInVehicle"; break;
        }
        return new string[] { s };
    }

    public void setItem(ItemRadio item) {
        this.radioItem = item;

        this.state = EnumState.WAIT;

        this.playClipOnRadio(ItemRadio.EnumClip.SENDING_HELP);
    }

    public EnumState getState() {
        return this.state;
    }

    private void playClipOnRadio(ItemRadio.EnumClip clip) {
        if(this.radioItem != null) {
            this.radioItem.playAudioClip(clip);
        }
    }

    public enum EnumState {
        FIND_RADIO = 0,
        WAIT = 1,
        GET_IN_VEHICLE = 2,
    }
}
