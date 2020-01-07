using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ZPanic/Items/ItemBasic", order = 1)]
public class ItemData : ScriptableObject {

    public const int MAX_ID = 127;

    [SerializeField]
    private string unlocalizedName = "nul";
    [SerializeField]
    private GameObject prefab;

    [Space]
    [SerializeField]
    [Tooltip("")]
    private EnumWeaponAnimationType idleAnimationId;

    [SerializeField]
    [Header("Inventory")]
    private Vector3 inventoryPositionOffset = Vector3.zero;
    [SerializeField]
    private Vector3 inventoryRotation = new Vector3(22.5f, 45, 0f);
    [SerializeField]
    private float inventoryScale = 1f;

    [Header("In-Hand")]
    public Vector3 inHandPosition = Vector3.zero;
    public Vector3 inHandRotation = Vector3.zero;
    public float inHandScale = 1f;

    private Mesh mesh;
    private int materialNumber = -1;

    public void init() {
        if(this.prefab == null) {
            throw new Exception("Item \"" + this.unlocalizedName + "\" has no prefab set!");
        }
    }

    public string getUnlocalizedName() {
        return this.unlocalizedName;
    }

    public GameObject getPrefab() {
        return this.prefab;
    }

    public MutableTransform getContainerMT() {
        return new MutableTransform(this.inventoryPositionOffset, Quaternion.Euler(this.inventoryRotation), new Vector3(this.inventoryScale, this.inventoryScale, this.inventoryScale));
    }
    
    public Mesh getMesh() {
        if(this.mesh == null) {
            this.mesh = this.prefab.GetComponent<MeshFilter>().sharedMesh;
        }
        return this.mesh;
    }

    public int getMaterialNumber() {
        if(this.materialNumber == -1) {
            this.materialNumber = this.prefab.GetComponent<MeshRenderer>().sharedMaterial == References.list.itemMaterialLit_0 ? 0 : 1;
        }
        return this.materialNumber;
    }

    public EnumWeaponAnimationType getIdleAnimation() {
        return this.idleAnimationId;
    }
}
