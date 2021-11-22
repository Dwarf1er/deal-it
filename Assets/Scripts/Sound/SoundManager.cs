using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour, ISubscriber {
    void Start() {
        EventManager.Get()
            .Subscribe((ClassStartEvent classStartEvent) => OnClassStart(classStartEvent))
            .Subscribe((ClassEndEvent classEndEvent) => OnClassEnd(classEndEvent))
            .Subscribe((AlertEvent alertEvent) => OnAlert(alertEvent))
            .Subscribe((SoundEvent soundEvent) => OnSound(soundEvent))
            .Subscribe((PanelInputEvent inputEvent) => OnPanelInput(inputEvent))
            .Subscribe((ToggleEvent toggleEvent) => OnToggle(toggleEvent))
            .Subscribe((QuestStartEvent questEvent) => OnQuestStart(questEvent))
            .Subscribe((QuestEndEvent questEvent) => OnQuestEnd(questEvent))
            .Subscribe((TaskEndEvent taskEvent) => OnTaskEnd(taskEvent));
    }

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);    
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return null;
    }

    private void PlayAt(string path, Vector3 position) {
        GameObject soundPrefab = (GameObject)Resources.Load("Sounds/" + path);
        Instantiate(soundPrefab, position, soundPrefab.transform.rotation);
    }

    private void Play(string path) {
        PlayAt(path, Vector3.zero);
    }

    private void OnSound(SoundEvent soundEvent) {
        Play(soundEvent.GetSound());
    }

    private void OnToggle(ToggleEvent toggleEvent) {
        if(toggleEvent.GetTarget().name.ToLower().Contains("door")) {
            Play("Door Open");
        }
    }

    private void OnPanelInput(PanelInputEvent inputEvent) {
        Play("Swoosh");
    }

    private void OnQuestStart(QuestStartEvent questEvent) {
        Play("Ding");
    }

    private void OnQuestEnd(QuestEndEvent questEvent) {
        Play("Erase");
    }

    private void OnClassStart(ClassStartEvent classStartEvent) {
        Play("School Bell");
    }

    private void OnClassEnd(ClassEndEvent classStartEvent) {
        Play("School Bell");
    }

    private void OnAlert(AlertEvent alertEvent) {
        Play("Alert");
    }

    private void OnTaskEnd(TaskEndEvent taskEvent) {
        // TODO: Play so not overlappign OnQuestEnd.
        // Play("Erase");
    }
}
