using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour {
    public Text title;

    private void Update() {
        title.text = PlayerPrefs.GetString("EndTitle", "Default") + " Ending";
    }
}
