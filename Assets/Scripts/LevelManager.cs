using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private static LevelManager singleton;
    public static string[] levels = new string[] { "Level 1", "Level 2", "Level 3", "Level 4" };
    public static int levelsCompleted = 0;

    void Awake() {
        if (singleton != null && singleton != this) {
            Destroy(gameObject);
        }
        else {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static void goToTheNextLevel() {
        SceneManager.LoadScene(levels[levelsCompleted]);
    }

    public static void incrementLevelsCompleted() {
        levelsCompleted++;
    }
}
