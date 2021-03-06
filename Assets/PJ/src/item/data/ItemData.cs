﻿using System;
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
    private EnumWeaponAnimationType idleAnimationId = EnumWeaponAnimationType.NO_WEAPON;

    [Header("Inventory")]
    [SerializeField]
    private Vector2 inventoryPosition = Vector2.zero;
    [SerializeField]
    private Vector3 inventoryRotation = new Vector3(0f, 0f, 0f);
    [SerializeField]
    private float inventoryScale = 1f;

    [Header("In-Hand")]
    public Vector3 inHandPosition = Vector3.zero;
    public Vector3 inHandRotation = Vector3.zero;
    public float inHandScale = 1f;

    [Space]
    //public bool hasDurability = false;
    //public int maxDurability = 0;

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
        if(this.prefab == null) {
            throw new Exception("Item \"" + this.unlocalizedName + "\" has no prefab set!");
        }
        return this.prefab;
    }

    public MutableTransform getContainerMT() {
        return new MutableTransform(this.inventoryPosition, Quaternion.Euler(this.inventoryRotation), Vector3.one * this.inventoryScale);
    }
    
    public Mesh getMesh() {
        if(this.mesh == null) {
            this.mesh = this.prefab.GetComponentInChildren<MeshFilter>().sharedMesh; // Hacky way to get mesh for meta items
        }
        return this.mesh;
    }

    public int getMaterialNumber() {
        if(this.materialNumber == -1) {
            MeshRenderer mr = this.getPrefab().GetComponent<MeshRenderer>();
            if(mr == null) {
                // Meta item?
                mr = this.getPrefab().GetComponentInChildren<MeshRenderer>();
            }

            this.materialNumber = mr.sharedMaterial == References.list.itemMaterialLit_0 ? 0 : 1;
        }
        return this.materialNumber;
    }

    public EnumWeaponAnimationType getIdleAnimation() {
        return this.idleAnimationId;
    }
}
