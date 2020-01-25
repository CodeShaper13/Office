using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ItemRadio : ItemBase<ItemData> {

    [SerializeField]
    private AudioSource audioSource = null;

    [Space]

    [SerializeField]
    private AudioClip sendingHelpClip = null;
    [SerializeField]
    private AudioClip comingSoonClip = null;
    [SerializeField]
    private AudioClip arrivedClip = null;

    public override void onRightClick(Player player) {
        base.onRightClick(player);

        GameModeBase gm = Director.singleton.getCurrentGameMode();
        if(gm != null && gm is GameModeCallForHelp) {
            GameModeCallForHelp gmch = (GameModeCallForHelp)gm;
            if(gmch.getState() == GameModeCallForHelp.EnumState.FIND_RADIO) {
                gmch.setItem(this);
            }
        }
    }

    public void playAudioClip(EnumClip clip) {
        if(this.audioSource != null) {

            AudioClip ac = null;
            switch(clip) {
                case EnumClip.SENDING_HELP:
                    ac = this.sendingHelpClip;
                    break;
                case EnumClip.SOON:
                    ac = this.comingSoonClip;
                    break;
                case EnumClip.ARRRIVED:
                    ac = this.arrivedClip;
                    break;
            }

            if(ac != null) {
                this.audioSource.PlayOneShot(ac);
            }
        }
    }

    public enum EnumClip {
        SENDING_HELP = 0,
        SOON = 1,
        ARRRIVED = 2,
    }
}
