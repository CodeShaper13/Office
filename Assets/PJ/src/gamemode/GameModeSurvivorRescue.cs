using UnityEngine;
using System;
using System.Collections.Generic;

[AddComponentMenu("ZPanic/GameMode/Game Mode Survivor Rescue")]
public class GameModeSurvivorRescue : GameModeBase {

    [SerializeField]
    [Tooltip("The locations that the survivors will spawn at.")]
    private Transform[] survivorSpawnPoints = null;
    [SerializeField]
    private GameObject survivorPrefab = null;

    private List<Survivor> survivors;

    public override void initGameMode(Director director) {
        base.initGameMode(director);

        if(this.survivorPrefab == null) {
            throw new Exception("survivorPrefab can not be null!");
        }

        this.survivors = new List<Survivor>();

        int skinIndex = 0;
        foreach(Transform t in this.survivorSpawnPoints) {
            if(t != null) {
                GameObject obj = GameObject.Instantiate(this.survivorPrefab, t.position, Quaternion.Euler(0, UnityEngine.Random.Range(0, 359), 0));
                Survivor survivor = obj.GetComponent<Survivor>();

                // Pick skin.
                survivor.GetComponent<RandomSkin>().pickSpecificSkin(skinIndex);
                skinIndex++;
            }
        }
    }

    public override void updateGameMode() {
        base.updateGameMode();
    }

    public override string[] getObjectiveText() {
        return new string[] { "gamemode.rescue.objective" };
    }
}
