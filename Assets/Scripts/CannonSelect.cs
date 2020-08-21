using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSelect : MonoBehaviour {
    public GameObject sliderPrefab;
    private GameObject slider;
    private GameObject knob;
    private float maxSlider;
    private float minSlider;
    private bool isBeingAimed;
    private bool isBeingAdjusted;

    public static float CANNON_MIN_VELOCITY = 5;
    public static float CANNON_MAX_VELOCITY = 10;
    private float KNOB_MAX_DISTANCE = 0.59375f;
    private float gradient;

    private void Start() {
        GameEvents.fireCannons.AddListener(removeTransparentSlider);
        gradient = (CANNON_MAX_VELOCITY - CANNON_MIN_VELOCITY) / (2 * KNOB_MAX_DISTANCE);
    }

    private void Update() {
        if (isBeingAimed) {
            if (!Input.GetMouseButton(0)) {
                isBeingAimed = false;
                GameEvents.cannonReleased.Invoke();
            }
            else {
                if (slider != null) {
                    Destroy(slider);
                }
                Vector2 mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouseVector.y, mouseVector.x) * Mathf.Rad2Deg);
            }
        }
        else if (isBeingAdjusted) {
            if (!Input.GetMouseButton(1)) {
                isBeingAdjusted = false;

                // Set the velocity based off the knob position.
                float difference = knob.transform.position.y - minSlider;
                float velocity = gradient * difference + CANNON_MIN_VELOCITY;
                GetComponent<CannonFire>().setCannonBallSpeed(velocity);

                Destroy(slider);
                GameEvents.cannonReleased.Invoke();
            }
            else {
                float yVal = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
                if (yVal > maxSlider) {
                    yVal = maxSlider;
                }
                else if (yVal < minSlider) {
                    yVal = minSlider;
                }

                knob.transform.position = new Vector3(knob.transform.position.x, yVal, 0);
            }
        }
    }

    public void startBeingAimed() {
        isBeingAimed = true;
    }

    public void startBeingAdjusted() {
        isBeingAdjusted = true;
        slider = Instantiate(sliderPrefab, transform.position + new Vector3(1.1f, 0, 0), Quaternion.identity);
        knob = slider.transform.GetChild(0).gameObject;
    }

    public void showTransparentSlider() {
        float velocity = GetComponent<CannonFire>().getVelocity();

        slider = Instantiate(sliderPrefab, transform.position + new Vector3(1.1f, 0, 0), Quaternion.identity);
        knob = slider.transform.GetChild(0).gameObject;
        maxSlider = slider.transform.position.y + KNOB_MAX_DISTANCE;
        minSlider = slider.transform.position.y - KNOB_MAX_DISTANCE;

        Color temp = slider.GetComponent<SpriteRenderer>().color;
        temp.a = 0.5f;
        slider.GetComponent<SpriteRenderer>().color = temp;

        temp = knob.GetComponent<SpriteRenderer>().color;
        temp.a = 0.5f;
        knob.GetComponent<SpriteRenderer>().color = temp;

        float grad = (2 * KNOB_MAX_DISTANCE) / (CANNON_MAX_VELOCITY - CANNON_MIN_VELOCITY);
        float c = -grad * ((CANNON_MAX_VELOCITY + CANNON_MIN_VELOCITY) / 2);
        knob.transform.position += new Vector3(0, grad * velocity + c, 0);
    }

    public void removeTransparentSlider() {
        Destroy(slider);
    }
}
