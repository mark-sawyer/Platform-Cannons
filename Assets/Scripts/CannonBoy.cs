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
    private float lastDirectionBuffer = 1;

    void Update() {

        switch (state) {
            case CannonBoyState.GROUNDED:
                if (Input.GetKeyDown("space")) {
                    rb.velocity = new Vector2(rb.velocity.x, JUMP_VELOCITY);
                    jumpSpriteChangeDelay = 0.1f;
                    state = CannonBoyState.JUMPING;
                    anim.SetTrigger("enter jump");
                }
                else {
                    anim.SetFloat("horizontal", Input.GetAxisRaw("Horizontal"));
                    rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), rb.velocity.y);
                }

                break;

            case CannonBoyState.JUMPING:
                anim.SetFloat("horizontal", rb.velocity.x);

                if (jumpSpriteChangeDelay > 0) {
                    jumpSpriteChangeDelay -= Time.deltaTime;
                }

                Collider2D platformHit = Physics2D.OverlapBox(bc.bounds.center, bc.bounds.size, 0f, platformLayerMask);

                if (platformHit != null && jumpSpriteChangeDelay <= 0) {
                    state = CannonBoyState.GROUNDED;
                    anim.SetTrigger("land jump");
                }
                break;
        }
    }
}

public enum CannonBoyState {
    GROUNDED,
    JUMPING
};
