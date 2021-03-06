﻿using UnityEngine;
using UnityEngine.AI;

public class NavHelper {

    public NavMeshAgent agent;

    public DoorBase door;
    public float timer;

    public NavHelper(Character monster) {
        this.agent = monster.GetComponent<NavMeshAgent>();
    }

    public void update() {
        if(this.door != null) {
            // Break down door
            this.timer += Time.deltaTime;

            if(this.timer > 1.25f) {
                this.timer = 0f;
                this.door.strength--;
                this.door.audioBreakingDoor.Play();
            }

            if(this.door.strength <= 0) {
                this.door.destroyDoor();

                this.agent.isStopped = false;
                this.door = null;
            }
        }
    }

    public void setDoor(DoorBase door) {
        this.door = door;
        this.agent.isStopped = true;
        this.agent.velocity = Vector3.zero;
    }

    /// <summary>
    /// Returns true the the destination was updated.
    /// </summary>
    public bool setDest(Vector3 pos, float stoppingDis = -1) {
        if(this.door == null) {
            this.agent.SetDestination(pos);
            if(stoppingDis != -1) {
                this.agent.stoppingDistance = stoppingDis;
            }
            return true;
        }
        return false;
    }

    public void clearPath() {
        this.agent.ResetPath();
    }
}
