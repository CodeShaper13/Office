using System;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour {

    public static Director singleton;

    [SerializeField]
    private GameObject zombiePrefab;

    [Space]

    [SerializeField]
    [Min(0)]
    [Tooltip("The minimum distance a zombie can spawn from a player.")]
    private int zombieMinSpawnDistance;
    [SerializeField]
    [Min(0)]
    [Tooltip("The maximum distance a zombie can spawn from a player.")]
    private int zombieMaxSpawnDistance;

    [Space]

    public Difficulty difficulty;

    /// <summary> How long the game has been going. </summary>
    public float timePlaying;

    private List<ZombieBase> allZombiesList;
    private Queue<ZombieSpawnArea> spawnAreaQueue;
    private GameModeBase gameMode;

    private void OnValidate() {
        // Correct invalid values for the min and max spawn distances.
        if(this.zombieMinSpawnDistance < 0) {
            this.zombieMinSpawnDistance = 0;
        }
        if(this.zombieMaxSpawnDistance < 0) {
            this.zombieMaxSpawnDistance = 0;
        }
        if(this.zombieMaxSpawnDistance <= this.zombieMinSpawnDistance) {
            this.zombieMaxSpawnDistance = this.zombieMinSpawnDistance + 1;
        }
    }

    private void Awake() {
        Director.singleton = this;

        if(this.zombiePrefab == null) {
            throw new Exception("Zombie prefab field can not be null!");
        } else if(!this.zombiePrefab.GetComponent<ZombieBase>()) {
            throw new Exception("Zombie prefab must contain a ZombieBase component!");
        }

        if(this.difficulty == null) {
            throw new Exception("Difficulty field can not be null!");
        }

        // Create the zombie list and put any zombies that were placed via the inspector into the list.
        this.allZombiesList = new List<ZombieBase>();
        foreach(ZombieBase zombie in GameObject.FindObjectsOfType<ZombieBase>()) {
            this.allZombiesList.Add(zombie);
        }

        // Construct a queue of all of the spawn areas.
        this.spawnAreaQueue = new Queue<ZombieSpawnArea>();
        foreach(ZombieSpawnArea sa in GameObject.FindObjectsOfType<ZombieSpawnArea>()) {
            if(sa.enabled && sa.gameObject.activeInHierarchy) {
                this.spawnAreaQueue.Enqueue(sa);
            }
        }
        this.spawnAreaQueue.Shuffle();

        // Setup the game mode.
        this.gameMode = GameObject.FindObjectOfType<GameModeBase>();
        if(this.gameMode == null) {
            Debug.LogWarning("No GameMode set in inspector!");
        } else {
            this.gameMode.initGameMode(this);
        }
    }

    private void Update() {
        if(!Pause.isPaused()) {
            this.timePlaying += Time.deltaTime;

            // Remove dead zombies from list.
            this.cleanZombieList();

            // Update the game mode object.
            if(this.gameMode != null) {
                this.gameMode.updateGameMode();
            }

            this.trySpawnZombies();
        }
    }

    /// <summary>
    /// Returns the number of zombies in the world.
    /// </summary>
    public int getZombieCount() {
        return this.allZombiesList.Count;
    }

    /// <summary>
    /// Returns a reference to the current game mode.  May be null.
    /// </summary>
    public GameModeBase getCurrentGameMode() {
        return this.gameMode;
    }

    /// <summary>
    /// Spawns a zombie at the passed position.
    /// </summary>
    public void spawnZombie(Vector3 position) {
        ZombieBase zombie = GameObject.Instantiate(this.zombiePrefab, position, Quaternion.Euler(0, UnityEngine.Random.Range(0, 359), 0), this.transform).GetComponent<ZombieBase>();
        this.allZombiesList.Add(zombie);
    }

    /// <summary>
    /// Removes dead zombies from the list.
    /// </summary>
    private void cleanZombieList() {
        ZombieBase z;
        for(int i = this.allZombiesList.Count - 1; i >= 0; i--) {
            z = this.allZombiesList[i];

            if(!z || z.health.isDead()) {
                this.allZombiesList.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Spawn more zombies to maintain the correct amount based off the threat level.
    /// </summary>
    private void trySpawnZombies() {
        int currentZombieCount = this.getZombieCount();
        int targetZombieCount = this.difficulty.getTargetZombieCount(this.timePlaying);

        if(targetZombieCount > currentZombieCount) {
            int checkedAreas = 0;

            while(currentZombieCount < targetZombieCount) {
                // Find a spot to spawn the zombie.

                if(checkedAreas >= this.spawnAreaQueue.Count) {
                    break;
                }

                ZombieSpawnArea area = this.spawnAreaQueue.Dequeue();
                checkedAreas++;

                bool valid = true;
                foreach(Player p in Main.singleton.playerManager.getAllPlayers<Player>()) {
                    if(!p.health.isDead()) {
                        float dis = Vector3.Distance(p.getFootPos(), area.transform.position);
                        if(dis < this.zombieMinSpawnDistance || dis > this.zombieMaxSpawnDistance) {
                            valid = false;
                            break;
                        }
                    }
                }

                if(valid) {
                    this.spawnZombie(area.getRndPoint());
                    currentZombieCount++;
                    this.spawnAreaQueue.Enqueue(area);
                }
            }
        }
    }
}
