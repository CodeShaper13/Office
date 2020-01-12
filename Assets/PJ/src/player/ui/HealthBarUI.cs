using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {

    [SerializeField]
    private Color healthGreen = Color.white;
    [SerializeField]
    private Color healthOrange = Color.white;
    [SerializeField]
    private Color healthRed = Color.white;
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Image healthSliderImage;

    public void updateHealthBar(Health health) {
        Color c;
        int hp = health.getHealth();
        if(hp > 50) {
            c = this.healthGreen;
        }
        else if(hp > 25) {
            c = this.healthOrange;
        }
        else {
            c = this.healthRed;
        }

        this.healthSliderImage.color = c;
        this.healthSlider.value = hp / 100f;

        /*
        this.healthImage.color = c;
        this.healthImage.fillAmount = hp / 100f;
        */
    }
}
