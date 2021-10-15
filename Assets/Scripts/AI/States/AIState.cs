using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState {
    protected AI ai;

    public AIState(AI ai) {
        this.ai = ai;
    }

    public bool IsNextState(float probability) {
        return Random.Range(0.0f, 1.0f) < probability;
    }

    /// Checks for state change.
    public abstract AIState NextState();

    /// Performed before first step.
    public abstract void Enter();

    /// Performs state.
    public abstract void Update();

    /// Performed on state change.
    public abstract void Exit();

    /// Checks if state is complete (if "wandering" state, always true).
    public abstract bool IsComplete();
}
