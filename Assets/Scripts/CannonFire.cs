using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire : MonoBehaviour {
    public CannonColour colour;
    private GameObject cannonBall;
    public Animator anim;
    private float cannonBallSpeed = 12.5f;

    private void Start() {
        GameEvents.fireCannons.AddListener(fire);
        anim = GetComponent<Animator>();

        switch (colour) {
            case CannonColour.PINK:
                anim.SetTrigger("pink");
                cannonBall = (GameObject)Resources.Load("Prefabs/Cannon Balls/cannon ball pink");
                break;
            case CannonColour.PURPLE:
                anim.SetTrigger("purple");
                cannonBall = (GameObject)Resources.Load("Prefabs/Cannon Balls/cannon ball purple");
                break;
            case CannonColour.BLUE:
                anim.SetTrigger("blue");
                cannonBall = (GameObject)Resources.Load("Prefabs/Cannon Balls/cannon ball blue");
                break;
            case CannonColour.GREEN:
                anim.SetTrigger("green");
                cannonBall = (GameObject)Resources.Load("Prefabs/Cannon Balls/cannon ball green");
                break;
            case CannonColour.ORANGE:
                anim.SetTrigger("orange");
                cannonBall = (GameObject)Resources.Load("Prefabs/Cannon Balls/cannon ball orange");
                break;
        }
    }

    private void fire() {
        anim.SetTrigger("fire");
        Vector3 direction = new Vector3(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad), 0);
        GameObject firedCannonBall = Instantiate(cannonBall, transform.position + direction, Quaternion.identity);
        firedCannonBall.GetComponent<CannonBall>().setInitialVelocity(cannonBallSpeed, direction);
    }

    public void setCannonBallSpeed(float speed) {
        cannonBallSpeed = speed;
    }

    public float getVelocity() {
        return cannonBallSpeed;
    }
}
