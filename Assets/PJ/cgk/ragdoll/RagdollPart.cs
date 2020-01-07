using UnityEngine;
using System;

public class RagdollPart : MonoBehaviour {

    [NonSerialized]
    public Rigidbody rigidbodyComponent;
    [NonSerialized]
    public Collider colliderComponent;

    private void Awake() {
        this.rigidbodyComponent = this.GetComponent<Rigidbody>();
        this.colliderComponent = this.GetComponent<Collider>();
        
        this.rigidbodyComponent.isKinematic = true;
    }

    /// <summary>
    /// Called when the Ragdoll becomes floppy.
    /// </summary>
    public virtual void onBecomeFloppy() { }
}