using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour {
    [SerializeField] private LayerMask cannonLayer;

    private void Start() {
        cannonLayer = LayerMask.GetMask("cannon");
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0, cannonLayer);
            if (ray.collider != null) {
                ray.collider.GetComponent<CannonSelect>().startBeingHeld();
            }
        }
    }
}