using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<CannonBoy>().beginNextLevelTransition();
            GetComponent<Animator>().SetTrigger("grabbed");
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
