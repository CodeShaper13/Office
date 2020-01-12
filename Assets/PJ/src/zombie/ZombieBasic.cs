using UnityEngine;

public class ZombieBasic : ZombieBase {

    [SerializeField]
    private SkinnedMeshRenderer[] skins;

    protected override void init() {
        base.init();

        // Disable all of the skins but the randomly picked one.
        int skinIndex = Random.Range(0, this.skins.Length);
        for(int j = 0; j < this.skins.Length; j++) {
            this.skins[j].gameObject.SetActive(j == skinIndex);
        }
    }
}
