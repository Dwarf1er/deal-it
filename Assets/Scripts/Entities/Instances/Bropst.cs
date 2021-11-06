using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bropst : MonoBehaviour, IDealable, ISubscriber, IQuestProvider {
    private IEndEvent endEvent;
    private SpriteRenderer spriteRenderer;
    private bool providedQuest = false;
    private Quest quest;

    private void Start() {
        quest = GameObject.Find("Bropst Quest").GetComponent<Quest>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        EventManager.Get()
            .Subscribe((InteractStartEvent interactEvent) => OnInteractStart(interactEvent))
            .Subscribe((DealStartEvent dealEvent) => OnDealStart(dealEvent))
            .Subscribe((DealEndEvent dealEvent) => OnDealEnd(dealEvent))
            .Subscribe((DialogueEndEvent dialogueEvent) => OnDialogueEnd(dialogueEvent));
    }

    public bool ProvidedQuest() {
        return providedQuest;
    }

    public Quest GetQuest() {
        return quest;
    }

    public bool IsDealable() {
        return quest.TasksDone() && !quest.IsComplete() && endEvent == null;
    }

    public bool IsInteractable() {
        return true;
    }

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    private void OnInteractStart(InteractStartEvent interactEvent) {
        if(!interactEvent.GetTo().Equals(this)) return;

        if(providedQuest) {
            EventManager.Get().Broadcast(new DialogueStartEvent("Bropst", "Come back when you got my coffee."));
        } else {
            EventManager.Get().Broadcast(new DialogueStartEvent("Bropst", "I need my coffee."));
            quest.Enter();
            providedQuest = true;
        }
    }

    private void OnDealStart(DealStartEvent dealEvent) {
        if(!dealEvent.GetTo().Equals(this)) return;
        endEvent = dealEvent.GetEndEvent();
    }

    private void OnDealEnd(DealEndEvent dealEvent) {
        if(endEvent != dealEvent) return;

        if(dealEvent.IsCancelled()) {
            endEvent = null;
            return;
        }

        DialogueStartEvent dialogueEvent = new DialogueStartEvent("Bropst", "My last dose... Time to join my friends...");
        endEvent = dialogueEvent.GetEndEvent();
        EventManager.Get().Broadcast(dialogueEvent);
    }
    
    private void OnDialogueEnd(DialogueEndEvent dialogueEvent) {
        if(endEvent != dialogueEvent) return;
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf() {
        Color color = spriteRenderer.color;

        for(int i = 0; i <= 100; i++) {
            color.a = Mathf.Lerp(1.0f, 0.0f, i / 100.0f);
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.005f);
        }

        quest.Exit();
        Destroy(this.gameObject);
    }

    public bool HasDistance() {
        return true;
    }
    
    public Vector2 GetPosition() {
        return transform.position;
    }

    public Transform GetTransform() {
        return transform;
    }
}