using UnityEngine;

[AddComponentMenu("ZPanic/GameMode/GM Repair")]
public class GameModeRepair : GameModeBase {

    private string objectiveName;

    public override string[] getObjectiveText() {
        return new string[] { "gamemode.repair.objective", this.objectiveName };
    }
}
