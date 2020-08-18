using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire : MonoBehaviour {
    public GameObject cannonBall;
    public Animator anim;

    private void Start() {
        GameEvents.fireCannons.AddListener(fire);
        anim = GetComponent<Animator>();
    }

    private void fire() {
        anim.SetTrigger("fire");
        Instantiate(cannonBall, transform.position, transform.rotation);
    }
}
