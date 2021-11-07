using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State {
    protected IStateHandler stateHandler;

    public State(IStateHandler stateHandler) {
        this.stateHandler = stateHandler;
    }

    public bool IsNextState(float probability) {
        return Random.Range(0.0f, 1.0f) < probability;
    }

    /// Checks for state change.
    public abstract State NextState();

    /// Performed before first step.
    public abstract void Enter();

    /// Performs state.
    public abstract void Loop();

    /// Performed on state change.
    public abstract void Exit();

    /// Checks if state is complete.
    public abstract bool IsComplete();
}
