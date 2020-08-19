using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSelect : MonoBehaviour {
    public GameObject sliderPrefab;
    private GameObject slider;
    private GameObject knob;
    private float maxSlider;
    private float minSlider;
    private float VELOCITY_FUNCTION_SLOPE = 12.63157895f;
    private bool isBeingAimed;
    private bool isBeingAdjusted;

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
                float velocity = VELOCITY_FUNCTION_SLOPE * difference + 5;
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
        maxSlider = slider.transform.position.y + 0.59375f;
        minSlider = slider.transform.position.y - 0.59375f;

        Color temp = slider.GetComponent<SpriteRenderer>().color;
        temp.a = 0.5f;
        slider.GetComponent<SpriteRenderer>().color = temp;

        temp = knob.GetComponent<SpriteRenderer>().color;
        temp.a = 0.5f;
        knob.GetComponent<SpriteRenderer>().color = temp;

        knob.transform.position += new Vector3(0, (19f / 240f) * velocity - (95f / 96f), 0);
    }

    public void removeTransparentSlider() {
        Destroy(slider);
    }
}
