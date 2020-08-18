using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSelect : MonoBehaviour {
    private bool isBeingHeld;

    private void Update() {
        if (isBeingHeld) {
            if (!Input.GetMouseButton(0)) {
                isBeingHeld = false;
                GameEvents.cannonReleased.Invoke();
            }
            else {
                Vector2 mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouseVector.y, mouseVector.x) * Mathf.Rad2Deg);
            }
        }
    }

    public void startBeingHeld() {
        isBeingHeld = true;
    }
}
