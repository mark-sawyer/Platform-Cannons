using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : MonoBehaviour {
    [SerializeField] private GameObject player;
    public bool drop;
    private float dropSpeed = -0.05f;

    private void Update() {
        if (drop) {
            transform.position += new Vector3(0, dropSpeed, 0);
            if (player != null && player.GetComponent<CannonBoy>().droppingWithBlue) {
                player.transform.position += new Vector3(0, dropSpeed, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            drop = true;
            player = collision.gameObject;
            LevelStageManager.aBlueHasDropped = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            player = null;
        }
    }
}
