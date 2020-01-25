using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScreenFadeIn : MonoBehaviour {

    public float timeAllBlack = 0f;
    public float fadeTime = 1f;
    public bool destroyOnFinish;

    private Image image;

    private void Awake() {
        this.image = this.GetComponent<Image>();
    }

    private void Update() {
        float f = Director.singleton.timePlaying - this.timeAllBlack;
        this.image.color = this.image.color.setAlpha(Mathf.Lerp(1, 0, f / this.fadeTime));

        // Destroy
        if(this.destroyOnFinish && f > (this.fadeTime + this.timeAllBlack)) {
            GameObject.Destroy(this.gameObject);
        }
    }
}
