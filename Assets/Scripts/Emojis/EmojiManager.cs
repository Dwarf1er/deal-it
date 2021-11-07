using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiManager : MonoBehaviour, ISubscriber {
    private Dictionary<IEvent, GameObject> activeEventEmojis = new Dictionary<IEvent, GameObject>();
    private void Start() {
        EventManager.Get().Subscribe((DealStartEvent dealEvent) => OnDealStart(dealEvent));
        EventManager.Get().Subscribe((DealEndEvent dealEvent) => OnDealEnd(dealEvent));
    }

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return this.transform;
    }

    private GameObject Spawn(string path, Vector3 position) {
        GameObject emojiPrefab = (GameObject)Resources.Load("Emojis/" + path);
        return Instantiate(emojiPrefab, position, emojiPrefab.transform.rotation);
    }

    private IEnumerator SpawnDestroyerEnumerator(GameObject gameObject, float delay) {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }

    private void SpawnAndDestory(string path, Vector3 position, float delay) {
        GameObject gameObject = Spawn(path, position);
        StartCoroutine(SpawnDestroyerEnumerator(gameObject, delay));
    }

    private void OnDealStart(DealStartEvent dealEvent) {
        activeEventEmojis.Add(dealEvent.GetEndEvent(), Spawn("Status", dealEvent.GetPosition()));
    }

    private void OnDealEnd(DealEndEvent dealEvent) {
        if(activeEventEmojis.ContainsKey(dealEvent)) {
            Destroy(activeEventEmojis[dealEvent]);
            activeEventEmojis.Remove(dealEvent);

            if(dealEvent.IsCancelled()) {
                SpawnAndDestory("Cross", dealEvent.GetPosition(), 1.0f);
            } else {
                SpawnAndDestory("Check", dealEvent.GetPosition(), 1.0f);
            }
        }
    }
}
