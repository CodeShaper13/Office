using UnityEngine;
using System;

[AddComponentMenu("ZPanic/GameMode/GM Escape")]
public class GameModeReachSafety : GameModeBase {

    [Tooltip("The name of the location the Players are going to.")]
    public string locationName = "Safe House";
    public SafeHouseTrigger safeHouseTrigger = null;

    public override void initGameMode() {
        base.initGameMode();

        if(this.safeHouseTrigger == null) {
            throw new Exception("safeHouseTrigger must be set!");
        }
    }

    public override void updateGameMode() {
        base.updateGameMode();

        if(this.safeHouseTrigger.allPlayersInHouse()) {
            this.triggerWin();
        }
    }

    public override string[] getObjectiveText() {
        return new string[] {"gamemode.reachSafety.objective", this.locationName };
    }
}
