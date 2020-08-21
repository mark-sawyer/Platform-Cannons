using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {
    private void Start() {
        GameEvents.unlockKey.AddListener(unlock);
        GameEvents.relockKey.AddListener(relock);
    }

    private void unlock() {
        GetComponent<Animator>().SetTrigger("disappear");
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void relock() {
        GetComponent<Animator>().SetTrigger("appear");
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void disableSprite() {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
