using System;
using UnityEngine;

public class ItemCannedFood : ItemEdible {

    [SerializeField]
    private MeshRenderer[] canMeshes;

    private EnumCanType type;

    public override void initializeItem() {
        base.initializeItem();

        var v = Enum.GetValues(typeof(EnumCanType));
        this.type = (EnumCanType)v.GetValue(UnityEngine.Random.Range(0, v.Length - 1));
    }

    public override string getItemName() {
        string s = this.data.getUnlocalizedName();
        switch(this.type) {
            case EnumCanType.BEANS:
                return s + ".beans";
            case EnumCanType.SOUP:
                return s + ".soup";
            case EnumCanType.TUNA:
                return s + ".tuna";
            case EnumCanType.CORN:
                return s + ".corn";
        }
        return s;
    }

    public override void showItem() {
        this.canMeshes[(int)this.type].enabled = true;
    }

    public override void hideItem() {
        this.canMeshes[(int)this.type].enabled = false;
    }

    private enum EnumCanType {
        BEANS,
        SOUP,
        TUNA,
        CORN,
    }
}
