using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour {
    private static Music music;

    private void Start() {
        DontDestroyOnLoad(this);
        if(music != null) Destroy(this.gameObject);
        music = this;
    }

    private void Update() {
        if(SceneManager.GetActiveScene().name == "End") {
            Destroy(this.gameObject);
        }
    }
}
