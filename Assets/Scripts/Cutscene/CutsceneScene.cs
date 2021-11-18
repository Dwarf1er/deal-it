using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneScene : CutsceneAbstract {
    private string sceneName;

    public CutsceneScene(string sceneName) {
        this.sceneName = sceneName;
    }

    public override void Enter() {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}