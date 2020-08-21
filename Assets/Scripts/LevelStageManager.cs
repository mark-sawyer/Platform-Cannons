using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStageManager : MonoBehaviour {
    public static LevelStage levelStage;
    private float timer;
    private bool waitingButtonNotPressed;
    public static bool doStuff;

    private float TIME_BEFORE_DISAPPEAR = 3;
    private float TIME_FOR_PLATFORM_TRANSFORM = 1.5f;
    private float TIME_FOR_CANNON_BOY_TO_APPEAR = 2.1f;
    private float TIME_FOR_PLATFORM_TO_DISAPPEAR = 1.1f;

    private void Update() {
        if (doStuff) {
            switch (levelStage) {
                case LevelStage.AIMING:
                    if (Input.GetKeyUp("space")) {
                        levelStage = LevelStage.FIRING;
                        timer = TIME_BEFORE_DISAPPEAR;
                    }
                    break;

                case LevelStage.FIRING:
                    timer -= Time.deltaTime;

                    if (Input.GetKeyDown("space")) {
                        GameEvents.platformation.Invoke();
                        levelStage = LevelStage.WAITING;
                        doStuff = false;
                        timer = TIME_FOR_PLATFORM_TRANSFORM;
                    }

                    else if (timer <= 0) {
                        GameEvents.disappearCannonBalls.Invoke();
                        levelStage = LevelStage.AIMING;
                    }
                    break;

                case LevelStage.WAITING:
                    if (Input.GetKeyDown("space")) {
                        GameEvents.appearCannonBoy.Invoke();  // For starting platform
                        levelStage = LevelStage.PLATFORMING;
                        doStuff = false;
                        timer = TIME_FOR_CANNON_BOY_TO_APPEAR;
                    }
                    else if (Input.GetKeyDown("r")) {
                        GameEvents.disappearPlatforms.Invoke();
                        levelStage = LevelStage.AIMING;
                        doStuff = false;
                        timer = TIME_FOR_PLATFORM_TO_DISAPPEAR;
                    }
                    break;
                    
                case LevelStage.PLATFORMING:
                    if (Input.GetKeyDown("r")) {
                        GameEvents.disappearPlatforms.Invoke();
                        GameEvents.disappearCannonBoy.Invoke();

                        levelStage = LevelStage.AIMING;
                        doStuff = false;
                        timer = TIME_FOR_PLATFORM_TO_DISAPPEAR;
                    }
                    break;
            }
        }

        else {
            timer -= Time.deltaTime;

            if (timer <= 0) {
                doStuff = true;
            }
        }
    }
}

public enum LevelStage {
    AIMING,
    FIRING,
    WAITING,
    PLATFORMING
};
