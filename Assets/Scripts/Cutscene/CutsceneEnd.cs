using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneEnd : CutsceneAbstract {
    private string endTitle;

    public CutsceneEnd(string endTitle) {
        this.endTitle = endTitle;
    }

    public override void Enter() {
        PlayerPrefs.SetString("EndTitle", endTitle);
        SceneManager.LoadScene("End", LoadSceneMode.Single);
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}