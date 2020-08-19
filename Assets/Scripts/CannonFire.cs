using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire : MonoBehaviour {
    public GameObject cannonBall;
    public Animator anim;
    [SerializeField] private float cannonBallSpeed = 5;

    private void Start() {
        GameEvents.fireCannons.AddListener(fire);
        anim = GetComponent<Animator>();
    }

    private void fire() {
        anim.SetTrigger("fire");
        Vector3 direction = new Vector3(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad), 0);
        GameObject firedCannonBall = Instantiate(cannonBall, transform.position + direction, Quaternion.identity);
        firedCannonBall.GetComponent<CannonBall>().setInitialVelocity(cannonBallSpeed, direction);
    }
}
