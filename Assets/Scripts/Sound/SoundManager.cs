using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour, ISubscriber {
    void Start() {
        EventManager.Get()
            .Subscribe((ClassStartEvent classStartEvent) => OnClassStart(classStartEvent))
            .Subscribe((ClassEndEvent classEndEvent) => OnClassEnd(classEndEvent))
            .Subscribe((AlertEvent alertEvent) => OnAlert(alertEvent));
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return null;
    }

    private void PlayAt(string path, Vector3 position) {
        GameObject soundPrefab = (GameObject)Resources.Load(path);
        Instantiate(soundPrefab, position, soundPrefab.transform.rotation);
    }

    private void Play(string path) {
        PlayAt(path, Vector3.zero);
    }

    private void OnClassStart(ClassStartEvent classStartEvent) {
        Play("Sounds/School Bell");
    }

    private void OnClassEnd(ClassEndEvent classStartEvent) {
        Play("Sounds/School Bell");
    }

    private void OnAlert(AlertEvent alertEvent) {
        Play("Sounds/Alert");
    }
}
