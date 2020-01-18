using UnityEngine;

[DisallowMultipleComponent]
public abstract class GameModeBase : MonoBehaviour {


    private void Start() {
        this.initGameMode();
    }

    private void Update() {
        if(!Pause.isPaused()) {
            this.updateGameMode();
        }
    }

    /// <summary>
    /// Returns a reference to the current game mode.
    /// </summary>
    public static GameModeBase getCurrentGameMode() {
        GameModeBase gameMode = GameObject.FindObjectOfType<GameModeBase>();
        return gameMode;
    }

    /// <summary>
    /// Call to trigger a win of the map.
    /// </summary>
    public void triggerWin() {
        this.onWin();
    }

    /// <summary>
    /// Call to trigger a lose of the map.
    /// </summary>
    public void triggerLose() {
        this.onLose();
    }

    public virtual void initGameMode() { }

    public virtual void updateGameMode() { }

    public virtual void onWin() { }

    public virtual void onLose() { }

    /// <summary>
    /// Returns the text to show while the game is paused.
    /// </summary>
    public abstract string[] getObjectiveText();
}
