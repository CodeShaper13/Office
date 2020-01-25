using UnityEngine;
using System.Collections;

public class ThreatLevel : MonoBehaviour {

    public Difficulty difficulty;

    public float timePlaying;

    private void Update() {
        if(!Pause.isPaused()) {
            this.timePlaying += Time.deltaTime;
        }
    }



    /// <summary>
    /// Returns the threat level calculated from the time and threat level grow mode.
    /// </summary>
    public float getThreatLevel() {
        return 0; // Mathf.Pow(speed, p);
    }
}
