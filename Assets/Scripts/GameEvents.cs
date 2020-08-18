using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour {
    public static UnityEvent cannonReleased = new UnityEvent();
    public static UnityEvent fireCannons = new UnityEvent();
}
