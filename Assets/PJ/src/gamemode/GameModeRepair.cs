using UnityEngine;

[AddComponentMenu("ZPanic/GameMode/Game Mode Repair")]
public class GameModeRepair : GameModeBase {

    private string objectiveName;

    public override string[] getObjectiveText() {
        return new string[] { "gamemode.repair.objective", this.objectiveName };
    }
}
