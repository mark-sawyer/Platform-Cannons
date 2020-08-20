using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStageManager : MonoBehaviour {
    public static LevelStage levelStage;
    private float timer;
    private float TIME_BEFORE_DISAPPEAR = 3;

    private void Start() {
        GameEvents.fireCannons.AddListener(goToFiring);

        timer = TIME_BEFORE_DISAPPEAR;
    }

    private void Update() {
        switch (levelStage) {
            case LevelStage.FIRING:
                timer -= Time.deltaTime;

                if (Input.GetKeyDown("space")) {
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
                break;

            case LevelStage.PLATFORMING:
                break;
        }

        if (levelStage == LevelStage.FIRING) {
            
        }
    }

    private void goToFiring() {
        levelStage = LevelStage.FIRING;
    }
}

public enum LevelStage {
    AIMING,
    FIRING,
    WAITING,
    PLATFORMING
};
