using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {
    public Rigidbody2D rb;
    [SerializeField] private float initialVelocity;

    public void setInitialVelocity(float v, Vector2 direction) {
        rb.velocity = direction * v;
    }
}
