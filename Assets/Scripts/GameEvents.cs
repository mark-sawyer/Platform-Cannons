using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour {
    public static UnityEvent cannonReleased = new UnityEvent();
    public static UnityEvent fireCannons = new UnityEvent();
    public static UnityEvent disappearCannonBalls = new UnityEvent();
    public static UnityEvent platformation = new UnityEvent();
    public static UnityEvent appearCannonBoy = new UnityEvent();
    public static UnityEvent disappearPlatforms = new UnityEvent();
    public static UnityEvent disappearCannonBoy = new UnityEvent();
}
