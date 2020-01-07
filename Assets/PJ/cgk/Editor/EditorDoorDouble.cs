using UnityEditor;

[CustomEditor(typeof(DoorDouble))]
public class EditorDoorDouble : EditorDoorBase {

    protected SerializedProperty rightOpenState;
    protected SerializedProperty rightClosedState;
    protected SerializedProperty leftOpenState;
    protected SerializedProperty leftClosedState;

    private void OnEnable() {
        this.rightOpenState = this.serializedObject.FindProperty("rightOpenState");
        this.rightClosedState = this.serializedObject.FindProperty("rightClosedState");
        this.leftOpenState = this.serializedObject.FindProperty("leftOpenState");
        this.leftClosedState = this.serializedObject.FindProperty("leftClosedState");
    }

    public override void markAsClosed(DoorBase door) {
        this.rightClosedState.quaternionValue = ((DoorDouble)door).right.transform.localRotation;
        this.leftClosedState.quaternionValue = ((DoorDouble)door).left.transform.localRotation;
    }

    public override void markAsOpen(DoorBase door) {
        this.rightOpenState.quaternionValue = ((DoorDouble)door).right.transform.localRotation;
        this.leftOpenState.quaternionValue = ((DoorDouble)door).left.transform.localRotation;
    }
}
