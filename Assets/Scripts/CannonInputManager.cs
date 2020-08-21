using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonInputManager : MonoBehaviour {
    private LayerMask cannonLayer;
    public static bool canFire = true;
    private GameObject cannonMouseIsOver;

    private void Start() {
        GameEvents.cannonReleased.AddListener(setCanFire);
        GameEvents.fireCannons.AddListener(resetCannonMouseIsOver);

        cannonLayer = LayerMask.GetMask("cannon");
    }

    void Update() {
        if (LevelStageManager.levelStage == LevelStage.AIMING && canFire) {
            if (Input.GetKeyDown("space")) {
                GameEvents.fireCannons.Invoke();
            }
            else {
                RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0, cannonLayer);
                if (ray.collider != null) {
                    if (cannonMouseIsOver == null) {
                        cannonMouseIsOver = ray.collider.gameObject;
                        cannonMouseIsOver.GetComponent<CannonSelect>().showTransparentSlider();
                    }

                    if (Input.GetMouseButtonDown(0)) {
                        canFire = false;
                        cannonMouseIsOver.GetComponent<CannonSelect>().startBeingAimed();
                    }
                    else if (Input.GetMouseButtonDown(1)) {
                        canFire = false;
                        cannonMouseIsOver.GetComponent<CannonSelect>().removeTransparentSlider();
                        cannonMouseIsOver.GetComponent<CannonSelect>().startBeingAdjusted();
                    }
                }
                else if (cannonMouseIsOver != null) {
                    cannonMouseIsOver.GetComponent<CannonSelect>().removeTransparentSlider();
                    cannonMouseIsOver = null;
                }
            }
        }
    }

    private void setCanFire() {
        canFire = true;
        RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0, cannonLayer);
        if (ray.collider != null && ray.collider.gameObject == cannonMouseIsOver) {
            cannonMouseIsOver.GetComponent<CannonSelect>().showTransparentSlider();
        }
    }

    private void resetCannonMouseIsOver() {
        cannonMouseIsOver = null;
    }
}