using UnityEngine;

public class DoorSliding : DoorBase {

    [HideInInspector]
    public Vector3 closedState;
    [HideInInspector]
    public Vector3 openState;

    [Header("How fast the doors opens.  (This is units per second)")]
    public float openSpeed = 2.5f;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(this.openState, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.closedState, 0.1f);
    }

    public override bool detectIfOpen() {
        return Vector3.Distance(this.transform.localPosition, this.openState) < 0.1f;
    }

    protected override void updateDoor() {
        Vector3 targ = (this.isOpen ? this.openState : this.closedState);

        if(this.transform.localPosition != targ) {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, targ, this.openSpeed * Time.deltaTime);
        }
    }

    public override void setAsOpen() {
        this.transform.localPosition = this.openState;
    }

    public override void setAsClosed() {
        this.transform.localPosition = this.closedState;
    }
}
