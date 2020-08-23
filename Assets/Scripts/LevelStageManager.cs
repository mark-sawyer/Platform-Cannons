using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStageManager : MonoBehaviour {
    public static GameObject modeText;
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
        modeText = GameObject.Find("mode text");
    }

    private void Update() {
        if (doStuff) {
            switch (levelStage) {
                case LevelStage.FIRING:
                    if (beenThroughAFrame) {
                        timer -= Time.deltaTime;

                        if (Input.GetKeyDown("r")) {
                            GameEvents.disappearCannonBalls.Invoke();
                            changeLevelStage(LevelStage.AIMING);
                        }

                        else if (Input.GetKeyDown("space")) {
                            GameEvents.platformation.Invoke();
                            changeLevelStage(LevelStage.WAITING);
                            doStuff = false;
                            beenThroughAFrame = false;
                            timer = TIME_FOR_PLATFORM_TRANSFORM;
                        }

                        else if (timer <= 0) {
                            GameEvents.disappearCannonBalls.Invoke();
                            changeLevelStage(LevelStage.AIMING);
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
                            changeLevelStage(LevelStage.PLATFORMING);
                            doStuff = false;
                            timer = TIME_FOR_CANNON_BOY_TO_APPEAR;
                        }
                        else if (Input.GetKeyDown("r")) {
                            GameEvents.disappearPlatforms.Invoke();
                            changeLevelStage(LevelStage.AIMING);
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
                        Lock.locked = true;

                        changeLevelStage(LevelStage.AIMING);
                        doStuff = false;
                        timer = TIME_FOR_PLATFORM_TO_DISAPPEAR;
                        aBlueHasDropped = false;
                    }
                    else if (Input.GetKeyDown("e")) {
                        GameEvents.disappearCannonBoy.Invoke();
                        GameEvents.appearCannonBoy.Invoke();
                        GameEvents.relockKey.Invoke();
                        Lock.locked = true;
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
        changeLevelStage(LevelStage.FIRING);
        beenThroughAFrame = false;
        timer = TIME_BEFORE_DISAPPEAR;
    }

    public static void changeLevelStage(LevelStage ls) {
        levelStage = ls;
        modeText.GetComponent<Text>().text = getStageText(ls);
    }

    public static string getStageText(LevelStage ls) {
        string s = "";
        switch (ls) {
            case LevelStage.AIMING:
                s = "Left click on cannon: aim\n" +
                    "Right click on cannon: adjust power\n" +
                    "Space: fire";
                break;
            case LevelStage.FIRING:
                s = "Space: make platform\n" +
                    "R: aim again";
                break;
            case LevelStage.WAITING:
                s = "Space: confirm\n" +
                    "R: aim again";
                break;
            case LevelStage.PLATFORMING:
                s = "Space: jump\n" +
                    "R: aim again\n" +
                    "E: reset Cannonboy";
                break;
        }

        return s;
    }
}

public enum LevelStage {
    AIMING,
    FIRING,
    WAITING,
    PLATFORMING
};
