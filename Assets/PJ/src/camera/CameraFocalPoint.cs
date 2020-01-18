using Cinemachine;
using System;
using UnityEditor;
using UnityEngine;

public class CameraFocalPoint : MonoBehaviour {

    private PlayerManager pManager;

    [SerializeField]
    private RestricedAxis restrictedX;
    [SerializeField]
    [Tooltip("Restricting the Y will stop the focal point from traveling above or below a certain point on the y axis.")]
    private RestricedAxis restrictedY;
    [SerializeField]
    private RestricedAxis restrictedZ;

    private void Awake() {
        CinemachineVirtualCamera vc = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        vc.LookAt = this.transform;
        vc.Follow = this.transform;
    }

    private void Start() {
        this.pManager = Main.singleton.playerManager;
    }

    private void Update() {
        Vector3 v = this.pManager.getPlayerCenterpoint() + Vector3.up;

        // Clamp the Y position between the min and max.
        float clampedX = this.func(v.x, this.restrictedX);
        float clampedY = this.func(v.y, this.restrictedY);
        float clampedZ = this.func(v.z, this.restrictedZ);

        this.transform.position = new Vector3(clampedX, clampedY, clampedZ);
    }

    private float func(float f, RestricedAxis ra) {
        return ra.restriced ? Mathf.Clamp(f, ra.min, ra.max) : f;
    }

    private void OnDrawGizmos() {
        Vector3 v = this.transform.position;

        if(this.restrictedX.restriced) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(this.restrictedX.min, v.y, v.z), new Vector3(this.restrictedX.max, v.y, v.z));
        }
        if(this.restrictedY.restriced) {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(new Vector3(v.x, this.restrictedY.min, v.z), new Vector3(v.x, this.restrictedY.max, v.z));
        }
        if(this.restrictedZ.restriced) {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(new Vector3(v.x, v.y, this.restrictedY.min), new Vector3(v.x, v.y, this.restrictedY.max));
        }
    }

    [Serializable]
    private struct RestricedAxis {

        [SerializeField]
        public bool restriced;
        public float min;
        public float max;
    }

    [CustomEditor(typeof(CameraFocalPoint))]
    public class CameraFocalPointEditor : Editor {
        public override void OnInspectorGUI() {
            CameraFocalPoint fp = target as CameraFocalPoint;

            this.func(ref fp.restrictedX, "X");
            this.func(ref fp.restrictedY, "Y");
            this.func(ref fp.restrictedZ, "Z");

            if(GUI.changed) {
                EditorUtility.SetDirty(fp);
            }
        }

        private void func(ref RestricedAxis ra, string s) {
            ra.restriced = EditorGUILayout.Toggle("Restrict " + s, ra.restriced);
            if(ra.restriced) {
                EditorGUI.indentLevel = 1;
                EditorGUILayout.LabelField("Minimum " + s + ":", ra.min.ToString());
                EditorGUILayout.LabelField("Maximum " + s + ":", ra.max.ToString());
                EditorGUILayout.MinMaxSlider(ref ra.min, ref ra.max, -50, 50);
                EditorGUI.indentLevel = 0;
            }
        }
    }
}