using UnityEngine;

public class RenderHelper {

    /// <summary>
    /// Renders an ItemStack on the screen.  Used by containers.
    /// </summary>
    public static void renderItemMesh(IItemBase item, Vector3 position, Quaternion rotation, Vector3 scale, Camera camera = null) {
        MutableTransform mt = item.getData().getContainerMT();
        mt.position += position;
        mt.rotation *= rotation;
        mt.scale = new Vector3(mt.scale.x * scale.x, mt.scale.y * scale.y, mt.scale.z * scale.z);

        // Figure out what material to use.
        Material material = item.getData().getMaterialNumber() == 0 ? References.list.itemMaterialUnlit_0 : References.list.itemMaterialUnlit_1;
        Graphics.DrawMesh(item.getData().getMesh(), mt.toMatrix4x4(), material, 9, camera, 0, null, false, false);
    }
}
