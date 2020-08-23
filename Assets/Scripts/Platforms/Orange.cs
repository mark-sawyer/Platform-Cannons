using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        if (Lock.locked && collision.gameObject.tag == "Player") {
            GameEvents.unlockKey.Invoke();
            Lock.locked = false;
        }
    }
}
