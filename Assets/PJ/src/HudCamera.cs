using UnityEngine;

public class HudCamera : MonoBehaviour {

    private static Camera cameraObj;

    private void Awake() {
        HudCamera.cameraObj = this.GetComponent<Camera>();
    }

    public static Camera getCamera() {
        return HudCamera.cameraObj;
    }
}
