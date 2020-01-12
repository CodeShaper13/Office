using UnityEngine;

public class ItemDataThrowable : ItemData {

    [Space]

    [Tooltip("How far the grenade can be thrown.")]
    public float throwForce;
    [Tooltip("How much force is put on physics objects.")]
    public float rigidbodyForce = 1f;
    [Tooltip("The force mode to use when adding force to physicis objects.")]
    public ForceMode forceMode = ForceMode.Impulse;
}
