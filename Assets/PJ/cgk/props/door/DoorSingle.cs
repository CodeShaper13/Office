using UnityEngine;

public class DoorSingle : DoorBase {

    [SerializeField]
    private Quaternion openState;
    [SerializeField]
    private Quaternion closedState;

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
