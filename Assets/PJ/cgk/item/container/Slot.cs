using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Attached to the button GameObject that represents each slot in a container.
/// </summary>
public class Slot : MonoBehaviour, IPointerClickHandler {

    /// <summary> The slot index within the container that this coresponds to. </summary>
    private int index;
    /// <summary> Reference to the container that this slot belongs to. </summary>
    private Container container;
    private Text slotText;

    private void Awake() {
        this.slotText = this.GetComponentInChildren<Text>();
    }

    public void setFields(int index, Container owner) {
        this.index = index;
        this.container = owner;
    }

    /// <summary>
    /// Called by Unity because we implement IPointerClickHandler when the game object is clicked.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData) {
        if(this.isInteractable()) {
            bool leftBtn = eventData.button == PointerEventData.InputButton.Left;
            bool rightBtn = eventData.button == PointerEventData.InputButton.Right;
            bool middleBtn = eventData.button == PointerEventData.InputButton.Middle;

            this.container.onSlotClick(this.index, leftBtn, rightBtn, middleBtn);
        }
    }

    /// <summary>
    /// Renders the slot contents on the screen.
    /// </summary>
    public virtual void renderSlotContents() {
        IItemBase item = this.container.getContents().getItem(this.index);
        if(item != null) {
            RenderHelper.renderItemMesh(item, this.transform.position, this.transform.rotation, this.transform.localScale);
        }
    }

    public void setSlotText(string text) {
        this.slotText.text = text;
    }

    /// <summary>
    /// Returns true if the slot can be interacted with.
    /// This method is ment to be overriden in child classes.
    /// </summary>
    public virtual bool isInteractable() {
        return true;
    }
}