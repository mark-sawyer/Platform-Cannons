using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private static LevelManager singleton;

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
        SceneManager.LoadScene("Level 2");
    }
}
