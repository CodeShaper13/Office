using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DoorBase))]
public abstract class EditorDoorBase : Editor {

    protected SerializedProperty openState;
    protected SerializedProperty closedState;

    private void OnEnable() {
        this.openState = this.serializedObject.FindProperty("openState");
        this.closedState = this.serializedObject.FindProperty("closedState");
    }

    public override void OnInspectorGUI() {
        this.DrawDefaultInspector();

        this.serializedObject.UpdateIfRequiredOrScript();

        DoorBase door = (DoorBase)this.target;

        if(GUILayout.Button("Mark Transform as Open")) {
            this.markAsOpen(door);
        }
        if(GUILayout.Button("Mark Transfrom as Closed")) {
            this.markAsClosed(door);
        }

        if(GUILayout.Button("Open Door")) {
            door.setAsOpen();
        }
        if(GUILayout.Button("Close Door")) {
            door.setAsClosed();
        }

        this.serializedObject.ApplyModifiedProperties();
    }

    public abstract void markAsOpen(DoorBase door);

    public abstract void markAsClosed(DoorBase door);
}
