using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {
    public Rigidbody2D rb;
    public Animator anim;
    [SerializeField] private float initialVelocity;

    private void Start() {
        GameEvents.disappearCannons.AddListener(disappear);
    }

    public void setInitialVelocity(float v, Vector2 direction) {
        rb.velocity = direction * v;
    }

    private void disappear() {
        anim.SetTrigger("disappear");
    }

    private void destroy() {
        Destroy(gameObject);
    }
}
