using UnityEngine;

public class Main : MonoBehaviour {

    public static Main singleton;

    public PlayerManager playerManager = null;
    public UIManager uiManager = null;

    private void Awake() {
        if(Main.singleton == null) {
            GameObject.DontDestroyOnLoad(this.gameObject);
            Main.singleton = this;
        }
        else {
            GameObject.Destroy(this.gameObject);
            return;
        }

        References.bootstrap();
    }

    private void Start() {
        // Temp
        Vector3 pos;
        PlayerSpawnPoint psp = GameObject.FindObjectOfType<PlayerSpawnPoint>();
        if(psp != null) {
            pos = psp.transform.position;
        } else {
            pos = Vector3.up;
        }

        this.playerManager.createPlayer<Player>(pos);
    }

    private void Update() {

    }
}