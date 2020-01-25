using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : UIBase {

    [SerializeField]
    private Text objectiveText = null;

    public override void onShow() {
        base.onShow();

        // Set the objective text.
        GameModeBase gameMode = Director.singleton.getCurrentGameMode();
        if(gameMode == null) {
            this.objectiveText.text = "No Game Mode";
        } else {
            string[] s = gameMode.getObjectiveText();
            string key = "???";
            List<string> pars = new List<string>(s);
            if(s.Length >= 1) {
                key = s[0];
                pars.RemoveAt(0);
            }
            this.objectiveText.text = I18n.translation(key, pars.ToArray());
        }

        Pause.pause();
    }

    public override void onClose() {
        base.onClose();

        Pause.unPause();
    }

    public override void onEscapeOrBack() {
        this.callback_resume();
    }

    public void callback_resume() {
        this.manager.closeCurrent();
    }

    public void callback_exit() {
        print("Exiting Game...");
    }
}
