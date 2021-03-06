﻿using UnityEngine;

public class UIManager : MonoBehaviour {

    public UILevelSelect lvlSelectUi;
    public UIOptions optionsUi;
    public UIPause pauseUi;

    /// <summary> The UI that is currenlty open.  May be null. </summary>
    private UIBase currentUI;

    private bool visibleWhenOpened;

    private void Awake() {
        #if UNITY_EDITOR
            // Hide the UI objects if one got left open by accident.
            foreach(UIBase ui in this.GetComponentsInChildren<UIBase>()) {
                ui.gameObject.SetActive(false);
            }
        #endif
    }

    /// <summary>
    /// Closes the current UI and calls the UiBase#onHide() method for cleanup.
    /// </summary>
    public void closeCurrent() {
        if(this.currentUI != null) {
            this.currentUI.gameObject.SetActive(false);
            this.currentUI.onClose();
            this.currentUI = null;
        }

        // Restore the cursor to how it was when the ui was opened.
        if(this.visibleWhenOpened) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    /// <summary>
    /// Opens the passed UI and then returns it.
    /// </summary>
    public UIBase openUI(UIBase ui) {
        this.closeCurrent();

        this.visibleWhenOpened = Cursor.visible;

        // Unlock and show the cursor if it's locked.
        if(!Cursor.visible) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        this.currentUI = ui;
        ui.gameObject.SetActive(true);
        ui.onShow();
        return ui;
    }

    /// <summary>
    /// May be null.
    /// </summary>
    public UIBase getCurrentUI() {
        return this.currentUI;
    }
}
