using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStageManager : MonoBehaviour {
    public static LevelStage levelStage;
    private float timer;
    private float TIME_BEFORE_DISAPPEAR = 3;

    private void Start() {
        timer = TIME_BEFORE_DISAPPEAR;
    }

    private void Update() {
        print(levelStage);

        switch (levelStage) {
            case LevelStage.AIMING:
                if (Input.GetKeyUp("space")) {
                    levelStage = LevelStage.FIRING;
                }
                break;

            case LevelStage.FIRING:
                timer -= Time.deltaTime;

                if (Input.GetKeyDown("space")) {
                    print("key down in firing");
                    GameEvents.platformation.Invoke();
                    levelStage = LevelStage.WAITING;
                }

                else if (timer <= 0) {
                    GameEvents.disappearCannons.Invoke();
                    timer = TIME_BEFORE_DISAPPEAR;
                    levelStage = LevelStage.AIMING;
                }
                break;

            case LevelStage.WAITING:
                if (Input.GetKeyDown("space")) {
                    GameEvents.appearCannonBoy.Invoke();
                    levelStage = LevelStage.PLATFORMING;
                }
                break;

            case LevelStage.PLATFORMING:
                break;
        }

        if (levelStage == LevelStage.FIRING) {
            
        }
    }
}

public enum LevelStage {
    AIMING,
    FIRING,
    WAITING,
    PLATFORMING
};
