using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteHelper : MonoBehaviour {

    private PostProcessVolume volume;
    private Vignette vignetteLayer;
    private Color startVigColor = Color.white;

    [SerializeField]
    private Health health;
    [SerializeField]
    private Color nearDeathColor;

    private void Awake() {
        this.volume = gameObject.GetComponent<PostProcessVolume>();
        this.volume.profile.TryGetSettings(out this.vignetteLayer);

        this.startVigColor = this.vignetteLayer.color.value;
    }

    private void Update() {
        // Low hp color tint
        int hp = this.health.getHealth();
        if(hp < 25) {
            this.vignetteLayer.color.Interp(this.nearDeathColor, this.startVigColor, hp / 25f);
        } else {
            this.vignetteLayer.color.value = this.startVigColor;
        }
    }
}
