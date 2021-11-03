using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneScene : CutsceneAbstract {
    public string sceneName;

    public override void Enter() {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}