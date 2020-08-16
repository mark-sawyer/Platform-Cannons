using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBoy : MonoBehaviour {
    public static CannonBoyState state;
    public LayerMask platformLayerMask;
    public BoxCollider2D bc;
    public Rigidbody2D rb;
    public Animator anim;

    [SerializeField] private float JUMP_VELOCITY = 5;
    private float jumpSpriteChangeDelay;

    void Update() {
        switch (state) {
            case CannonBoyState.STANDING:
                if (Input.GetKeyDown("space")) {
                    rb.velocity = new Vector2(0, JUMP_VELOCITY);
                    enterJump();
                }
                else if (Input.GetAxisRaw("Horizontal") != 0) {
                    rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
                    state = CannonBoyState.RUNNING;
                    anim.SetTrigger("start running");
                }
                else {
                    rb.velocity = Vector2.zero;
                }
                break;

            case CannonBoyState.RUNNING:
                if (Input.GetKeyDown("space")) {
                    rb.velocity = new Vector2(rb.velocity.x, JUMP_VELOCITY);
                    enterJump();
                }
                else if (Input.GetAxisRaw("Horizontal") == 0) {
                    rb.velocity = Vector2.zero;
                    state = CannonBoyState.STANDING;
                    anim.SetTrigger("stop running");
                }
                break;

            case CannonBoyState.JUMPING:
                if (jumpSpriteChangeDelay > 0) {
                    jumpSpriteChangeDelay -= Time.deltaTime;
                }

                Collider2D platformHit = Physics2D.OverlapBox(bc.bounds.center, bc.bounds.size, 0f, platformLayerMask);

                if (platformHit != null && jumpSpriteChangeDelay <= 0) {
                    state = CannonBoyState.STANDING;
                    anim.SetTrigger("land jump");
                }
                break;
        }
    }

    private void enterJump() {
        jumpSpriteChangeDelay = 0.1f;
        state = CannonBoyState.JUMPING;
        anim.SetTrigger("enter jump");
    }
}

public enum CannonBoyState {
    STANDING,
    RUNNING,
    JUMPING
};
