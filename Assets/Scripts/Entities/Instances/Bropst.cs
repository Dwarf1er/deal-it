using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bropst : MonoBehaviour, IDealable, ISubscriber {
    private IEndEvent endEvent;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        EventManager.Get()
            .Subscribe((DealStartEvent dealEvent) => OnDealStart(dealEvent))
            .Subscribe((DealEndEvent dealEvent) => OnDealEnd(dealEvent))
            .Subscribe((DialogueEndEvent dialogueEvent) => OnDialogueEnd(dialogueEvent));
    }

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    private void OnDealStart(DealStartEvent dealEvent) {
        if(endEvent != null) return;
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