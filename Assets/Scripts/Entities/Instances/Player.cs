using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateHuman, IDealer {
    private ControlState controlState;

    protected override void Start() {
        this.controlState = new ControlState(this);

        base.Start();

        EventManager.Get()
            .Subscribe((MoveInputEvent inputEvent) => controlState.OnMoveInput(inputEvent))
            .Subscribe((DealInputEvent inputEvent) => controlState.OnDealInput(inputEvent))
            .Subscribe((InteractInputEvent inputEvent) => controlState.OnInteractInput(inputEvent))
            .Subscribe((StartEvent startEvent) => OnStart(startEvent));
    }

    private void OnStart(StartEvent startEvent) {
        SetNextState(controlState);
    }

    public override State GetBaseState() {
        return new LookAtState(this, Vector2.zero);
    }

    protected override string GetTextureName() {
        return "student1";
    }

    public override float GetSpeed() {
        return 0.8f;
    }
}
