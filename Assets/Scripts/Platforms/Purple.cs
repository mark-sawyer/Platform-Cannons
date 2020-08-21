using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 velocity = rb.velocity;
            rb.velocity = new Vector2(rb.velocity.x, 10);
        }
    }
}
