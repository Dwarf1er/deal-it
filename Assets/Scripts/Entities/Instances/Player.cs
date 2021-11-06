using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractHuman, IDealer {
    private IEndEvent endEvent;

    public override void Start() {
        this.textureName = "dealer1";

        base.Start();

        this.speed = 0.8f;

        EventManager.Get()
            .Subscribe((MoveInputEvent inputEvent) => OnMoveInputEvent(inputEvent))
            .Subscribe((DealInputEvent inputEvent) => OnDealInputEvent(inputEvent))
            .Subscribe((InteractInputEvent inputEvent) => OnInteractInputEvent(inputEvent))
            .Subscribe((DealStartEvent dealEvent) => OnDealStartEvent(dealEvent))
            .Subscribe((DealEndEvent dealEvent) => OnDealEndEvent(dealEvent));
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

        this.LookAt(dealEvent.GetTo().GetTransform());
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

    private Tuple<T, float> GetNearestType<T>() where T: IWithPosition {
        Tuple<T, float> minValue = null;

        foreach(MonoBehaviour monoBehaviour in FindObjectsOfType<MonoBehaviour>()) {
            if(monoBehaviour.TryGetComponent<T>(out T type)) {
                float distance = Vector2.Distance(this.GetPosition(), type.GetPosition());
                if(minValue == null || distance < minValue.second) {
                    minValue = new Tuple<T, float>(type, distance);
                }
            }
        }

        return minValue;
    }

    private static readonly float INTERACT_DISTANCE = 0.5f;

    private void OnDealInputEvent(DealInputEvent inputEvent) {
        if(inputEvent.player != this.transform.name) return;
        if(endEvent != null) return;

        Tuple<IDealable, float> minDealable = GetNearestType<IDealable>();

        if(minDealable == null) return;
        if(minDealable.second > INTERACT_DISTANCE) return;
        if(!minDealable.first.IsDealable()) return;

        EventManager.Get().Broadcast(new DealStartEvent(this, minDealable.first));
    }

    private void OnInteractInputEvent(InteractInputEvent inputEvent) {
        if(inputEvent.player != this.transform.name) return;
        if(endEvent != null) return;

        Tuple<IInteractable, float> minInteractable = GetNearestType<IInteractable>();

        if(minInteractable == null) return;
        if(minInteractable.second > INTERACT_DISTANCE) return;
        if(!minInteractable.first.IsInteractable()) return;

        EventManager.Get().Broadcast(new InteractStartEvent(this, minInteractable.first));
    }
}
