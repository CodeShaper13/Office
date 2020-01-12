using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class ItemThrowable<T> : ItemBase<T> where T : ItemDataThrowable {

    protected void throwItem(Player player) {
        this.gameObject.layer = Layers.DEFAULT; // Reset the layer so the flying item hits stuff.

        player.inventory.setItem(player.hotbarIndex.get(), null);
        Transform camTrans = player.getCamera().transform;
        this.setInWorld(true, camTrans.position + (camTrans.forward * 0.5f), camTrans.rotation);

        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;        

        rb.isKinematic = false;
        rb.AddForce(camTrans.forward * data.throwForce, ForceMode.Impulse);  // TODO make more realistic?

        BoxCollider col = this.GetComponent<BoxCollider>();
        col.enabled = true;
    }
}
