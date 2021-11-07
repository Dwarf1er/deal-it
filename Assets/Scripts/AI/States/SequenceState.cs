using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceState : State {
    private Queue<State> states;

    public SequenceState(IStateHandler stateHandler, State[] states) : base(stateHandler) {
        this.states = new Queue<State>(states);
    }

    public override State NextState() {
        if(this.IsComplete()) return stateHandler.GetBaseState();

        return this;
    }

    public override void Enter() {
        if(this.states.Count > 0) {
            this.states.Peek().Enter();
        }
    }

    public override void Loop() {
        if(this.states.Count == 0) return;

        State state = this.states.Peek();

        state.Loop();

        if(state.IsComplete()) {
            state.Exit();
            this.states.Dequeue();
            if(this.states.Count > 0) this.states.Peek().Enter();
        }
    }

    public override void Exit() {
        if(this.states.Count > 0) {
            this.states.Peek().Exit();
        }
    }

    public override bool IsComplete() {
        return states.Count == 0;
    }
}
