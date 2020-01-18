using UnityEngine;

public class RandomSkin : MonoBehaviour {

    [SerializeField]
    private SkinnedMeshRenderer[] skins = null;
    [SerializeField]
    [Tooltip("When the random skin is picked.  Set to never if a script should control this.")]
    private EnumPickTime time = EnumPickTime.NEVER;

    private void Awake() {
        if(this.time == EnumPickTime.AWAKE) {
            this.pickRandomSkin();
        }
    }

    private void Start() {
        if(this.time == EnumPickTime.START) {
            this.pickRandomSkin();
        }
    }

    /// <summary>
    /// Picks a random skin from the list of skinned mesh renderers.
    /// All others are disabled.
    /// </summary>
    public void pickRandomSkin() {
        this.pickSpecificSkin(Random.Range(0, this.skins.Length));
    }

    public void pickSpecificSkin(int skinIndex) {
        // Disable all of the skins but the picked one.
        for(int j = 0; j < this.skins.Length; j++) {
            if(skins != null) {
                this.skins[j].gameObject.SetActive(j == skinIndex);
            }
        }
    }
    
    public int getSkinCount() {
        return this.skins.Length;
    }

    public enum EnumPickTime {
        AWAKE = 1,
        START = 2,
        NEVER = 0,
    }
}
