using UnityEngine;

public class CharacterSelectionMenu : MonoBehaviour
{

    public GameObject[] characterPrefabs;
    private Vector2 center;
    private Vector2 offset = new Vector2(0.32f,0);
    private GameObject[] characters;
    private int selectedCharacterIndex;

    void Start() {
        characters = new GameObject[characterPrefabs.Length];
        for(int i = 0; i < characterPrefabs.Length; i++) {
            characters[i] = Instantiate(characterPrefabs[i], center + offset * (i - characterPrefabs.Length/2), Quaternion.identity);
        }
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {
            selectedCharacterIndex = (selectedCharacterIndex + 1) % characters.Length;
        }

        for (int i = 0; i < characterPrefabs.Length; i++) {
            int positionIndex = (i + selectedCharacterIndex) % characters.Length;
            Vector3 targetPosition = (center + offset * (positionIndex - characterPrefabs.Length / 2));
            if (Vector3.Distance(targetPosition, characters[i].transform.position) > 0.01f)
                characters[i].transform.position += (targetPosition - characters[i].transform.position).normalized * 3f * Time.deltaTime;

            Vector3 targetScale = new Vector3(1, 1, 1);

            if (positionIndex == characters.Length / 2)
                targetScale = new Vector3(2, 2, 1);
         
            if (Vector3.Distance(targetScale, characters[i].transform.localScale) > 0.01f)
                characters[i].transform.localScale += (targetScale - characters[i].transform.localScale).normalized * 10f * Time.deltaTime;
        }
    }
}
