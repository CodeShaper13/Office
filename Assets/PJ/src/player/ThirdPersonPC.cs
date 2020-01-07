using UnityEngine;
using System.Collections;

public class ThirdPersonPC {

    private float jumpPower = 8f;
    private float verticalRot;
    private Player player;
    private CharacterController cc;
    private float verticalVelocity = 0f;

    public ThirdPersonPC(Player player, CharacterController cc) {
        this.player = player;
        this.cc = cc;
    }

    public void update(bool allowLook, bool allowMove) {
        float mouseSens = 4f;
        float moveSpeed = 20f;

        // Let the player jump, only if they are on the ground.
        if(this.player.isOnGround()) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                this.verticalVelocity = this.jumpPower;
                this.player.anim.SetBool("Jump_b", true);
            }
        }

        Vector3 motion = new Vector3();
        this.cc.transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSens, 0);
        this.verticalRot -= Input.GetAxis("Mouse Y");
        this.verticalRot = Mathf.Clamp(this.verticalRot, -60, 60);
        Camera.main.transform.localRotation = Quaternion.Euler(this.verticalRot, 0, 0);
        motion = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0, Input.GetAxis("Vertical") * moveSpeed);
        motion = this.cc.transform.rotation * motion;

        // Update velocity/falling
        this.verticalVelocity += Physics.gravity.y * Time.deltaTime;

        // Move the player.  Doesn't the aniamtion clip do this, so now they go extra fast...?
        this.cc.Move(motion.setY(this.verticalVelocity) * Time.deltaTime);
    }
}
