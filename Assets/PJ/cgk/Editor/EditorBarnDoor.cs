using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DoorSliding))]
public class EditorBarnDoor : EditorDoorBase {

    public override void OnInspectorGUI() {
        this.DrawDefaultInspector();

        DoorSliding door = (DoorSliding)this.target;

        if(GUILayout.Button("Set Open Pos (Green)")) {
            door.openState = door.transform.position;
        }
        if(GUILayout.Button("Set Closed Pos (Red)")) {
            door.closedState = door.transform.position;
        }
        if(GUILayout.Button("Detect Open State")) {
            door.isOpen = door.detectIfOpen();
        }
    }

    public override void markAsClosed(DoorBase door) {
        this.openState.vector3Value = door.transform.localPosition;
    }

    public override void markAsOpen(DoorBase door) {
        this.closedState.vector3Value = door.transform.localPosition;
    }
}
