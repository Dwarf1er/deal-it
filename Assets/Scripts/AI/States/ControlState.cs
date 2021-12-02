using System.Collections.Generic;
using UnityEngine;

public class ControlState : State {
    private Vector2 direction;
    private IInteractable[] interactables;
    private IDealable[] dealables;

    public ControlState(IStateHandler stateHandler) : base(stateHandler) {
        interactables = FindCollection<IInteractable>().ToArray();
        dealables = FindCollection<IDealable>().ToArray();
    }

    public override void Enter() {
        if(!(stateHandler is IDealer)) {
            throw new System.Exception("StateHandler needs to be IDealer.");
        }
    }

    public override void Loop() {
        stateHandler.SetDirection(direction);
        stateHandler.MoveTowards((Vector2)stateHandler.GetTransform().position + stateHandler.GetDirection());
    }

    public override State NextState() {
        return this;
    }

    public override bool IsComplete() {
        return false;
    }

    public override void Exit() {
        EventManager.Get().UnSubcribeAll(this);
    }

    public bool HasDistance() {
        return true;
    }

    public IInteractable GetInteractTarget() {
        Tuple<IInteractable, float> value = GetNearestType<IInteractable>(interactables);

        if(value == null) return null;
        if(value.second > INTERACT_DISTANCE) return null;
        if(!value.first.IsInteractable()) return null;

        return value.first;
    }

    public IDealable GetDealableTarget() {
        Tuple<IDealable, float> value = GetNearestType<IDealable>(dealables);

        if(value == null) return null;
        if(value.second > INTERACT_DISTANCE) return null;
        if(!value.first.IsDealable()) return null;

        return value.first;
    }

    public Transform GetTransform() {
        return stateHandler.GetTransform();
    }

    public void OnMoveInput(MoveInputEvent inputEvent) {
        if(inputEvent.player != stateHandler.GetTransform().name) return;

        direction = inputEvent.direction;
    }

    private List<T> FindCollection<T>() {
        List<T> collection = new List<T>();

        foreach(MonoBehaviour monoBehaviour in Object.FindObjectsOfType<MonoBehaviour>()) {
            if(monoBehaviour.TryGetComponent<T>(out T type)) {
                collection.Add(type);
            }
        }

        return collection;
    }

    private Tuple<T, float> GetNearestType<T>(T[] collection) where T: IWithPosition {
        Tuple<T, float> minValue = null;

        foreach(T t in collection) {
            float distance = Vector2.Distance(GetTransform().position, t.GetPosition());
            if(minValue == null || distance < minValue.second) {
                minValue = new Tuple<T, float>(t, distance);
            }
        }

        return minValue;
    }

    private static readonly float INTERACT_DISTANCE = 0.32f;

    public void OnDealInput(DealInputEvent inputEvent) {
        if(inputEvent.player != stateHandler.GetTransform().name) return;

        Tuple<IDealable, float> minDealable = GetNearestType<IDealable>(dealables);

        if(minDealable == null) return;
        if(minDealable.second > INTERACT_DISTANCE) return;
        if(!minDealable.first.IsDealable()) return;

        DealStartEvent dealEvent = new DealStartEvent((IDealer)stateHandler, minDealable.first);
        EventManager.Get().Broadcast(dealEvent);
    }

    public void OnInteractInput(InteractInputEvent inputEvent) {
        if(inputEvent.player != stateHandler.GetTransform().name) return;

        Tuple<IInteractable, float> minInteractable = GetNearestType(interactables);

        if(minInteractable == null) return;
        if(minInteractable.second > INTERACT_DISTANCE) return;
        if(!minInteractable.first.IsInteractable()) return;

        InteractStartEvent interactEvent = new InteractStartEvent(stateHandler, minInteractable.first);
        EventManager.Get().Broadcast(interactEvent);
    }
}
