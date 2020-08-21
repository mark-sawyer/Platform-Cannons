using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPlatform : MonoBehaviour {
    public GameObject cannonBoy;

    void Start() {
        GameEvents.appearCannonBoy.AddListener(appearCannonBoy);
    }

    private void appearCannonBoy() {
        Instantiate(cannonBoy, transform.position + new Vector3(0, 0.563f, 0), Quaternion.identity);
    }
}
