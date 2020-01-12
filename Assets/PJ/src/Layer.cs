using UnityEngine;

/// <summary>
/// List of all the Unity layers that are uesed.  Prevents typos and lets us search for uses of tags with Shift+F12.
/// </summary>
public static class Layers {

    // Built in.
    public static readonly int DEFAULT_MASK = LayerMask.GetMask("Default");
    public static readonly int TRANSPARENT_FX_MASK = LayerMask.GetMask("TransparentFX");
    public static readonly int IGNORE_RAYCAST_MASK = LayerMask.GetMask("Ignore Raycast");
    public static readonly int WATER_MASK = LayerMask.GetMask("Water");
    public static readonly int UI_MASK = LayerMask.GetMask("UI");
    // User Defined.
    public static readonly int POST_PROCESSING_MASK = LayerMask.GetMask("PostProcessing");
    public static readonly int UI_ITEMS_MASK = LayerMask.GetMask("UiItems");
    public static readonly int ITEMS_MASK = LayerMask.GetMask("Item");
    public static readonly int ENTITIES_MASK = LayerMask.GetMask("Entity");

    public const int DEFAULT = 0;
    public const int ITEM = 10;
}