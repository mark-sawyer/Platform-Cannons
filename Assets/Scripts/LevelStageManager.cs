using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStageManager : MonoBehaviour {
    private LevelStage levelStage;
    private float timer;
    private float TIME_BEFORE_DISAPPEAR = 3;

    private void Start() {
        GameEvents.fireCannons.AddListener(goToFiring);
        timer = TIME_BEFORE_DISAPPEAR;
    }

    private void Update() {
        if (levelStage == LevelStage.FIRING) {
            timer -= Time.deltaTime;

            if (timer <= 0) {
                GameEvents.disappearCannons.Invoke();
                timer = TIME_BEFORE_DISAPPEAR;
                levelStage = LevelStage.AIMING;
                CannonInputManager.canFire = true;
            }
        }
    }

    private void goToFiring() {
        levelStage = LevelStage.FIRING;
    }
}

public enum LevelStage {
    AIMING,
    FIRING,
    PLATFORMING
};
