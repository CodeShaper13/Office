using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "ZPanic/Difficulty", order = 1)]
public class Difficulty : ScriptableObject {

    public string difficultyName = "???";

    [Space]

    public EnumThreatIncreaseMode increaseMode;
    public float rate = 1f;

    [Space]

    [Tooltip("The number of zombies, where time is the Threat Level.")]
    public AnimationCurve zombieCount;
    [Tooltip("How long in minutes the curve is.  Once the time exceeds the length of the curve, then the max value is used.")]
    public float curveLength;

    /// <summary>
    /// Returns the number of zombies that should exist.
    /// </summary>
    public int getTargetZombieCount(float time) {
        float t = time / (curveLength * 60);
        return (int)(this.zombieCount.Evaluate(t) * 100);
    }
}
