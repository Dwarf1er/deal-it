using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceState : AIState {
    private Queue<AIState> states;

    public SequenceState(AI ai, AIState[] states) : base(ai) {
        this.states = new Queue<AIState>(states);
    }

    public override AIState NextState() {
        if(this.IsComplete()) return new IdleState(this.ai);

        return this;
    }

    public override void Enter() {
        if(this.states.Count > 0) {
            this.states.Peek().Enter();
        }
    }

    public override void Update() {
        if(this.states.Count == 0) return;

        AIState state = this.states.Peek();

        state.Update();

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
