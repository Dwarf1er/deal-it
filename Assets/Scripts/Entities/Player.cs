using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractHuman, IDealer {
    private IEndEvent endEvent;

    public override void Start() {
        base.Start();

        this.speed = 0.8f;

        EventManager.Get().Subscribe((MoveEvent inputEvent) => OnMoveEvent(inputEvent));
        EventManager.Get().Subscribe((DealStartEvent dealEvent) => OnDealStartEvent(dealEvent));
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

    private void OnMoveEvent(MoveEvent moveEvent) {
        if(moveEvent.player != this.transform.name) return;

        this.direction = moveEvent.direction;

        if(direction.magnitude > 0 && endEvent is ICancellableEvent) {
            ICancellableEvent cancellableEvent = (ICancellableEvent)endEvent;
            cancellableEvent.Cancel();
        }

        this.transform.position += new Vector3(this.direction.x, this.direction.y, 0) * Time.deltaTime * this.speed;
    }
}
