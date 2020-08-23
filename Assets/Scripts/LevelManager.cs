using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private static LevelManager singleton;
    public static string[] levels = new string[] { "Level 1", "Level 2", "Level 3", "Level 4",
                                                   "Level 5", "Level 6", "Level 7", "Level 8",
                                                   "Level 9", "Level 10", "Level 11", "Level 12"};
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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public static void goToTheNextLevel() {
        SceneManager.LoadScene(levels[levelsCompleted]);
        Lock.locked = true;
    }

    public static void incrementLevelsCompleted() {
        levelsCompleted++;
    }
}
