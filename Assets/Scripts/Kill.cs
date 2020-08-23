using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (LevelStageManager.aBlueHasDropped) {
                GameEvents.disappearPlatforms.Invoke();
                GameEvents.disappearCannonBoy.Invoke();
                GameEvents.relockKey.Invoke();
                Lock.locked = true;
                LevelStageManager.changeLevelStage(LevelStage.AIMING);
            }
            else {
                GameEvents.disappearCannonBoy.Invoke();
                GameEvents.appearCannonBoy.Invoke();
                GameEvents.relockKey.Invoke();
                Lock.locked = true;
            }
        }
    }
}
