using UnityEngine;
using UnityEngine.UI;

public class HotbarSquare : MonoBehaviour {

    [SerializeField]
    private Color selectedColor = Color.white;

    private Color normalColor;
    private Image img;

    private void Awake() {
        this.img = this.GetComponent<Image>();
        this.normalColor = this.img.color;
    }

    public void setSelected(bool selected) {
        this.img.color = selected ? this.selectedColor : this.normalColor;
    }
}
