using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonInputManager : MonoBehaviour {
    [SerializeField] private LayerMask cannonLayer;
    private bool canFire = true;

    private void Start() {
        GameEvents.cannonReleased.AddListener(setCanFire);

        cannonLayer = LayerMask.GetMask("cannon");
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0, cannonLayer);
            if (ray.collider != null) {
                canFire = false;
                ray.collider.GetComponent<CannonSelect>().startBeingHeld();
            }
        }

        if (Input.GetKeyDown("space") && canFire) {
            GameEvents.fireCannons.Invoke();
        }
    }

    private void setCanFire() {
        canFire = true;
    }
}