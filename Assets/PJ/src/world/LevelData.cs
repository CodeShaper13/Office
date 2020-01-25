using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ZPanic/LevelData", order = 1)]
public class LevelData : ScriptableObject {

    [Tooltip("The scene's name.")]
    public string sceneName;
    [Tooltip("The unlocalized level description.")]
    public string levelDescription;
    [Tooltip("")]
    public Texture2D previewImage;
}
