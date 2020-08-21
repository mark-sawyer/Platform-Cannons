using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : MonoBehaviour {
    public bool drop;
    private float dropSpeed = -0.05f;
    private GameObject player;

    private void Update() {
        if (drop) {
            transform.position += new Vector3(0, dropSpeed, 0);
            if (player.GetComponent<CannonBoy>().droppingWithBlue) {
                player.transform.position += new Vector3(0, dropSpeed, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            drop = true;
            player = collision.gameObject;
            player.GetComponent<CannonBoy>().droppingWithBlue = true;
            LevelStageManager.aBlueHasDropped = true;
        }
    }
}
