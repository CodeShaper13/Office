using UnityEngine;

[DisallowMultipleComponent]
public abstract class GameModeBase : MonoBehaviour {

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

    public virtual void initGameMode(Director director) { }

    public virtual void updateGameMode() { }

    public virtual void onWin() { }

    public virtual void onLose() { }

    /// <summary>
    /// Returns the text to show while the game is paused.
    /// </summary>
    public abstract string[] getObjectiveText();
}
