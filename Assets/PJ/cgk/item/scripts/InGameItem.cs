using UnityEngine;

public class InGameItem : MonoBehaviour {

    [SerializeField]
    private ItemData item;
    
    private void Start() {
        if(this.item != null) {
            IItemBase itemObj = ItemManager.create<IItemBase>(this.item, this.transform.position, this.transform.rotation);
        }
    }

    private void OnValidate() {
        this.gameObject.name = "InGameItem[" + (this.item != null ? this.item.getUnlocalizedName() : "NONE") + "]";
    }

    private void OnDrawGizmosSelected() {
        if(this.item != null) {
            GameObject prefab = this.item.getPrefab();
            if(prefab != null) {
                MeshFilter mf = prefab.GetComponent<MeshFilter>();
                if(mf != null) {
                    Gizmos.DrawMesh(mf.sharedMesh, this.transform.position, this.transform.rotation, Vector3.one);
                    return;
                }
            }
        }

        this.func();
    }

    private void OnDrawGizmos() {
        this.func();
    }

    private void func() {
        Gizmos.color = this.item == null ? Color.red : Color.green;
        Gizmos.DrawSphere(this.transform.position, 0.25f);
    }
}
