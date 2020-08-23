using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject platform;
    private bool disappearing;

    private void Start() {
        GameEvents.disappearCannonBalls.AddListener(disappear);
        GameEvents.platformation.AddListener(becomePlatform);
    }

    public void setInitialVelocity(float v, Vector2 direction) {
        rb.velocity = direction * v;
    }

    private void disappear() {
        anim.SetTrigger("disappear");
        disappearing = true;
    }

    private void becomePlatform() {
        if (!disappearing) {
            Instantiate(platform, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

public enum CannonColour {
    PINK,
    PURPLE,
    BLUE,
    GREEN,
    ORANGE
};