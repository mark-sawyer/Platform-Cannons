using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStageManager : MonoBehaviour {
    public static LevelStage levelStage;
    public static float timer;
    public static bool doStuff;
    public static bool beenThroughAFrame;
    public static bool aBlueHasDropped;

    private float TIME_BEFORE_DISAPPEAR = 3;
    private float TIME_FOR_PLATFORM_TRANSFORM = 1.5f;
    private float TIME_FOR_CANNON_BOY_TO_APPEAR = 1.0f;
    private float TIME_FOR_PLATFORM_TO_DISAPPEAR = 1.1f;

    private void Start() {
        GameEvents.fireCannons.AddListener(goToFiring);
    }

    private void Update() {
        if (doStuff) {
            print(levelStage);
            switch (levelStage) {
                case LevelStage.FIRING:
                    if (beenThroughAFrame) {
                        timer -= Time.deltaTime;

                        if (Input.GetKeyDown("r")) {
                            GameEvents.disappearCannonBalls.Invoke();
                            levelStage = LevelStage.AIMING;
                        }

                        else if (Input.GetKeyDown("space")) {
                            GameEvents.platformation.Invoke();
                            levelStage = LevelStage.WAITING;
                            doStuff = false;
                            beenThroughAFrame = false;
                            timer = TIME_FOR_PLATFORM_TRANSFORM;
                        }

                        else if (timer <= 0) {
                            GameEvents.disappearCannonBalls.Invoke();
                            levelStage = LevelStage.AIMING;
                        }
                    }
                    else {
                        beenThroughAFrame = true;
                    }
                    
                    break;

                case LevelStage.WAITING:
                    if (beenThroughAFrame) {
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
                    }
                    else {
                        beenThroughAFrame = true;
                    }
                    
                    break;
                    
                case LevelStage.PLATFORMING:
                    if (Input.GetKeyDown("r") || (aBlueHasDropped && Input.GetKeyDown("e"))) {
                        GameEvents.disappearPlatforms.Invoke();
                        GameEvents.disappearCannonBoy.Invoke();
                        GameEvents.relockKey.Invoke();

                        levelStage = LevelStage.AIMING;
                        doStuff = false;
                        timer = TIME_FOR_PLATFORM_TO_DISAPPEAR;
                        aBlueHasDropped = false;
                    }
                    else if (Input.GetKeyDown("e")) {
                        GameEvents.disappearCannonBoy.Invoke();
                        GameEvents.appearCannonBoy.Invoke();
                        GameEvents.relockKey.Invoke();
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

    private void goToFiring() {
        levelStage = LevelStage.FIRING;
        beenThroughAFrame = false;
        timer = TIME_BEFORE_DISAPPEAR;
    }
}

public enum LevelStage {
    AIMING,
    FIRING,
    WAITING,
    PLATFORMING
};
