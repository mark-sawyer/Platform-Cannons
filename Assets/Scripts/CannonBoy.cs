using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CannonBoy : MonoBehaviour {
    public LayerMask platformLayerMask;
    public BoxCollider2D bc;
    public Rigidbody2D rb;
    public Animator anim;

    private float JUMP_VELOCITY = 6;
    private float MOVE_SPEED = 5;
    private float horizontalMove;
    private bool facingRight = true;
    private bool grounded;
    private bool appeared;
    private bool starWasGrabbed;
    private float transitionTimer;
    private bool onGreen;

    private void Start() {
        GameEvents.disappearCannonBoy.AddListener(disappearCannonBoy);
    }

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
            if (Input.GetKeyDown("space") && grounded && !onGreen) {
                rb.velocity = new Vector2(rb.velocity.x, JUMP_VELOCITY);
            }
        }

        else if (starWasGrabbed) {
            transform.eulerAngles += new Vector3(0, 10, 0);
            if (transitionTimer <= 0) {
                if (Mathf.Abs(transform.localScale.x) > 0.02) {
                    if (facingRight) {
                        transform.localScale += new Vector3(-0.01f, 0, 0);
                    }
                    else {
                        transform.localScale += new Vector3(0.01f, 0, 0);
                    }
                }
                else {
                    LevelStageManager.levelStage = LevelStage.AIMING;
                    LevelManager.incrementLevelsCompleted();
                    LevelManager.goToTheNextLevel();
                }
            }
            else {
                transitionTimer -= Time.deltaTime;
            }
        }
    }


    private bool isGrounded() {
        Collider2D collider = Physics2D.OverlapBox(bc.bounds.center + new Vector3(0, -bc.bounds.size.y * 0.2f, 0),
                                                   bc.bounds.size + new Vector3(-0.1f, 0, 0), 0f, platformLayerMask);

        if (collider != null && collider.tag == "green") {
            onGreen = true;
        }
        else {
            onGreen = false;
        }

        return collider != null;
    }

    private void flipSprite() {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void setAppeared() {  // Called by animation end.
        appeared = true;
    }

    private void disappearCannonBoy() {
        appeared = false;
        rb.isKinematic = false;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("disappear");
    }

    private void destroySelf() {
        Destroy(gameObject);
    }

    public void beginNextLevelTransition() {
        starWasGrabbed = true;
        anim.SetBool("is airborne", true);
        transitionTimer = 2;
        appeared = false;
        rb.isKinematic = false;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
    }
}

