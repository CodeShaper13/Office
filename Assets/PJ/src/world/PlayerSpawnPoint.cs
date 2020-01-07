using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour {

    private void OnDrawGizmos() {
        float f = 0.25f;
        Gizmos.color = Color.blue;
        Vector3 v1 = new Vector3(f, 0, f);
        Vector3 v2 = new Vector3(-f, 0, -f);
        Vector3 v3 = new Vector3(-f, 0, f);
        Vector3 v4 = new Vector3(f, 0, -f);

        Gizmos.DrawLine(this.transform.position + v1, this.transform.position + v2);
        Gizmos.DrawLine(this.transform.position + v3, this.transform.position + v4);
    }
}
