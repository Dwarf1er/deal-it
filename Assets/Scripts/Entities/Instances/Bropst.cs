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
            .Subscribe((DealEndEvent dealEvent) => OnDealEnd(dealEvent));
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
        return endEvent == null;
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

        quest.Exit();
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