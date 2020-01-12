using UnityEngine;

public class RenderHelper {

    /// <summary>
    /// Renders an ItemStack on the screen.  Used by containers.
    /// </summary>
    public static void renderItemMesh(ItemData itemData, Vector3 position, Quaternion rotation, Vector3 scale, Camera camera = null) {
        MutableTransform mt = itemData.getContainerMT();
        mt.position += position;
        mt.rotation *= rotation;
        mt.scale = new Vector3(mt.scale.x * scale.x, mt.scale.y * scale.y, mt.scale.z * scale.z);

        // Move the mesh away from the GUI element
        mt.position += Vector3.forward * -2;

        // Figure out what material to use.
        Material material = itemData.getMaterialNumber() == 0 ? References.list.itemMaterialUnlit_0 : References.list.itemMaterialUnlit_1;
        Graphics.DrawMesh(itemData.getMesh(), mt.toMatrix4x4(), material, 9, camera, 0, null, false, false);

        Debug.DrawLine(new Vector3(200, 0, 0), mt.position, Color.yellow);
    }

    /// <summary>
    /// Renders an ItemStack on the screen.  Used by containers.
    /// </summary>
    public static void renderItemMesh(IItemBase item, Vector3 position, Quaternion rotation, Vector3 scale, Camera camera = null) {
        RenderHelper.renderItemMesh(item.getData(), position, rotation, scale, camera);
    }
}
