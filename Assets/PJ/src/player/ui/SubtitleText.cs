using UnityEngine;
using UnityEngine.UI;

public class SubtitleText : MonoBehaviour {

    [SerializeField]
    private Text text;

    private float timeVisible;

    private void Awake() {
        if(!Pause.isPaused()) {
            if(this.timeVisible > 0) {
                this.timeVisible -= Time.deltaTime;

                if(this.timeVisible <= 0) {
                    this.clearText();
                }
            }
        }
    }

    public void setText(string unlocalizedKey, float timeVisible = 1f) {
        this.text.text = I18n.translation(unlocalizedKey);
        this.timeVisible = timeVisible;
    }

    private void clearText() {
        this.text.text = string.Empty;
    }
}
