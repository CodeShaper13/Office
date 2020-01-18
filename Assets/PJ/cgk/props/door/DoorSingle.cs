using UnityEngine;

public class DoorSingle : DoorBase {

    [HideInInspector]
    [SerializeField]
    private Quaternion openState;
    [HideInInspector]
    [SerializeField]
    private Quaternion closedState;

    [Tooltip("How many degrees this door moves per seconds while opening or closing.")]
    public float openSpeed = 150f;

    public override bool detectIfOpen() {
        return Quaternion.Angle(this.transform.localRotation, this.openState) < 5;
    }

    protected override void updateDoor() {
        Quaternion targ = this.isOpen ? this.openState : this.closedState;

        if(this.transform.localRotation != targ) {
            this.transform.localRotation = Quaternion.RotateTowards(this.transform.localRotation, targ, this.openSpeed * Time.deltaTime);
        }
    }

    public override void setAsOpen() {
        this.transform.localRotation = this.openState;
    }

    public override void setAsClosed() {
        this.transform.localRotation = this.closedState;
    }
}
