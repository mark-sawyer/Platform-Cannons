using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEndDestroySelf : MonoBehaviour {
    private void destroySelf() {
        Destroy(gameObject);
    }
}
