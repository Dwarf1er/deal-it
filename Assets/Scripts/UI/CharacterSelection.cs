using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public string[] characterSprites;
    public GameObject humanPrefab;
    public Vector2 center;

    private Vector2 offset = new Vector2(0.32f, 0);
    private GameObject[] characters;
    private int selectedCharacterIndex;

    private void Start() {
        characters = new GameObject[characterSprites.Length];

        for(int i = 0; i < characterSprites.Length; i++) {
            characters[i] = Instantiate(humanPrefab, center + offset * (i - characterSprites.Length / 2), Quaternion.identity);
            characters[i].GetComponent<MenuHuman>().textureName = characterSprites[i];

            if(characterSprites[i] == PlayerPrefs.GetString("Skin")) {
                selectedCharacterIndex = i;
            }
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            selectedCharacterIndex = (selectedCharacterIndex + 1) % characterSprites.Length;
            PlayerPrefs.SetString("Skin", characterSprites[selectedCharacterIndex]);
            PlayerPrefs.Save();
        }

        for (int i = 0; i < characterSprites.Length; i++) {
            int positionIndex = (i + selectedCharacterIndex) % characters.Length;
            Vector3 targetPosition = (center + offset * (positionIndex - characterSprites.Length / 2));
            
            if (Vector3.Distance(targetPosition, characters[i].transform.position) > 0.01f) {
                characters[i].transform.position += (targetPosition - characters[i].transform.position).normalized * 3f * Time.deltaTime;
            }

            Vector3 targetScale = new Vector3(1, 1, 1);

            if (positionIndex == characters.Length / 2) {
                targetScale = new Vector3(2, 2, 1);
            }

            if (Vector3.Distance(targetScale, characters[i].transform.localScale) > 0.01f) {
                characters[i].transform.localScale += (targetScale - characters[i].transform.localScale).normalized * 10f * Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            SceneManager.LoadScene("Cutscene");
        }
    }
}
