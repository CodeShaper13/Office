using UnityEngine;

public class Area : MonoBehaviour {

    [HideInInspector]
    [SerializeField]
    public EnumRegionShape areaShape;
    [HideInInspector]
    [SerializeField]
    private Vector3 offset;

    // Fields for Point

    // Fields for Square
    [HideInInspector]
    [SerializeField]
    [Header("The diameter of the square on the x axis.")]
    private float xSize = 1;
    [HideInInspector]
    [SerializeField]
    [Header("The diameter of the square on the z axis.")]
    private float zSize = 1;

    // Fields for Circle
    [HideInInspector]
    [SerializeField]
    private float radius = 1;

    // Fields for Line
    [HideInInspector]
    [SerializeField]
    private float length = 1;

    private void OnDrawGizmos() {
        Gizmos.color = Colors.yellow;
        Vector3 v = this.getPos();
        switch(this.areaShape) {
            case EnumRegionShape.POINT:
                Gizmos.DrawWireSphere(v, 0.1f);
                break;
            case EnumRegionShape.SQUARE:
                Gizmos.DrawWireCube(v, new Vector3(this.xSize, 0, this.zSize));
                break;
            case EnumRegionShape.CIRCLE:
                GizmoHelpers.drawCircle(v, this.radius, 16);
                break;
            case EnumRegionShape.LINE:
                float half = this.length / 2;
                Gizmos.DrawLine(this.transform.position + this.transform.forward * half, this.transform.position - this.transform.forward * half);
                break;
        }
    }

    /// <summary>
    /// Returns a random point within the area.
    /// </summary>
    public Vector3 getRndPoint() {
        Vector3 pos = this.getPos();
        switch(this.areaShape) {
            case EnumRegionShape.POINT:
                return pos;
            case EnumRegionShape.SQUARE:
                float x = Random.Range(0, this.xSize) - (this.xSize / 2);
                float z = Random.Range(0, this.zSize) - (this.zSize / 2);
                return pos + this.transform.rotation * new Vector3(x, 0, z);
            case EnumRegionShape.CIRCLE:
                Vector2 v = Random.insideUnitCircle;
                return pos + new Vector3(v.x, 0, v.y) * this.radius;
            case EnumRegionShape.LINE:
                Vector3 lineStart = this.transform.position - this.transform.forward * (this.length / 2);
                return lineStart + this.transform.forward * Random.Range(0, length);
        }
        return this.transform.position;
    }

    /// <summary>
    /// Returns the orgin of this area.
    /// </summary>
    public Vector3 getPos() {
        return this.transform.position + this.offset;
    }

    /// <summary>
    /// Returns the size of the area.
    /// </summary>
    public float getSize() {
        switch(this.areaShape) {
            case EnumRegionShape.POINT:
                return 1;
            case EnumRegionShape.SQUARE:
                return this.xSize * this.zSize;
            case EnumRegionShape.CIRCLE:
                return Mathf.PI * this.radius * this.radius;
            case EnumRegionShape.LINE:
                return 0;  // Lines have no area?
        }
        return 0;
    }

    /// <summary>
    /// Returns the color to draw the area with in the Editor.
    /// Child classes can override this to preovide specific colors based on the use of the area.
    /// </summary>
    public virtual Color getColor() {
        return Colors.magenta;
    }
}
