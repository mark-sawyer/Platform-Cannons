using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBoy : MonoBehaviour {
    public LayerMask platformLayerMask;
    public BoxCollider2D bc;
    public Rigidbody2D rb;
    public Animator anim;

    [SerializeField] private float JUMP_VELOCITY = 6;
    [SerializeField] private float MOVE_SPEED = 5;
    private float horizontalMove;
    private bool facingRight = true;
    private bool grounded;
    private bool appeared;

    private void Update() {
        if (appeared) {
            grounded = isGrounded();
            anim.SetBool("is airborne", !grounded);

            // Movement
            horizontalMove = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontalMove * MOVE_SPEED, rb.velocity.y);
            anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
            if ((horizontalMove < 0 && facingRight) || (horizontalMove > 0 && !facingRight)) {
                flipSprite();
            }

            // Jumping
            if (Input.GetKeyDown("space") && grounded) {
                rb.velocity = new Vector2(rb.velocity.x, JUMP_VELOCITY);
            }
        }
    }


    private bool isGrounded() {
        Collider2D collider = Physics2D.OverlapBox(bc.bounds.center + new Vector3(0, -bc.bounds.size.y * 0.2f, 0), bc.size, 0f, platformLayerMask);
        return collider != null;
    }

    private void flipSprite() {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void setAppeared() {
        appeared = true;
    }
}

