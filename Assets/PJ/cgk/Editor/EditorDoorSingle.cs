using UnityEditor;

[CustomEditor(typeof(DoorSingle))]
public class EditorDoorSingle : EditorDoorBase {

    public override void markAsOpen(DoorBase door) {
        this.openState.quaternionValue = door.transform.localRotation;
    }

    public override void markAsClosed(DoorBase door) {
        this.closedState.quaternionValue = door.transform.localRotation;
    }
}
