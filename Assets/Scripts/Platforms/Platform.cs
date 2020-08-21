using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    public Animator anim;

    private void Start() {
        GameEvents.disappearPlatforms.AddListener(startDisappearAnimation);
    }

    private void startDisappearAnimation() {
        anim.SetTrigger("disappear");
    }

    private void destroySelf() {
        Destroy(gameObject);
    }
}
