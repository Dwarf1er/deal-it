using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractHuman, IDealer {
    private IEndEvent endEvent;

    public override void Start() {
        base.Start();

        this.speed = 0.8f;

        EventManager.Get().Subscribe((MoveInputEvent inputEvent) => OnMoveInputEvent(inputEvent));
        EventManager.Get().Subscribe((DealInputEvent inputEvent) => OnDealInputEvent(inputEvent));

        EventManager.Get().Subscribe((DealStartEvent dealEvent) => OnDealStartEvent(dealEvent));
        EventManager.Get().Subscribe((DealEndEvent dealEvent) => OnDealEndEvent(dealEvent));
    }

    /// TODO: Don't copy from other class.
    private bool CanStartEvent() {
        if(endEvent == null) return true;
        if(endEvent is ICancellableEvent) {
            ICancellableEvent cancellableEvent = (ICancellableEvent)endEvent;
            cancellableEvent.Cancel();
            endEvent = null;
            return true;
        }

        return false;
    }

    private void OnDealStartEvent(DealStartEvent dealEvent) {
        if(!dealEvent.GetFrom().Equals(this)) return;
        if(!CanStartEvent()) return;       

        endEvent = dealEvent.GetEndEvent();
    }

    private void OnDealEndEvent(DealEndEvent dealEvent) {
        if(!endEvent.Equals(dealEvent)) return;
        endEvent = null;
    }

    private void OnMoveInputEvent(MoveInputEvent inputEvent) {
        if(inputEvent.player != this.transform.name) return;

        this.direction = inputEvent.direction;

        if(direction.magnitude > 0 && endEvent is ICancellableEvent) {
            ICancellableEvent cancellableEvent = (ICancellableEvent)endEvent;
            cancellableEvent.Cancel();
        }

        this.transform.position += new Vector3(this.direction.x, this.direction.y, 0) * Time.deltaTime * this.speed;
    }

    private static readonly float DEAL_DISTANCE = 0.5f;

    private void OnDealInputEvent(DealInputEvent inputEvent) {
        if(inputEvent.player != this.transform.name) return;
        if(endEvent != null) return;

        IDealable minDeable = null;
        float minDistance = float.MaxValue;
        foreach(MonoBehaviour monoBehaviour in FindObjectsOfType<MonoBehaviour>()) {
            if(monoBehaviour.TryGetComponent<IDealable>(out IDealable dealable)) {
                float distance = Vector2.Distance(this.GetPosition(), dealable.GetPosition());
                if(distance < minDistance) {
                    minDistance = distance;
                    minDeable = dealable;
                }
            }
        }

        if(minDistance < DEAL_DISTANCE) {
            EventManager.Get().Broadcast(new DealStartEvent(this, minDeable));
        }
    }
}
