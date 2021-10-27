using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI : AbstractHuman {
    protected AIState state;
    private TilemapGraph graph;
    private float reachDistance = 0.025f;

    public override void Start() {
        base.Start();
        this.graph = FindObjectOfType<TilemapGraph>();
        this.state = new IdleState(this);
        this.state.Enter();
    }

    public TilemapGraph GetGraph() {
        return this.graph;
    }

    public bool ReachedPosition(Vector3 target) {
        return Vector2.Distance(target, this.transform.position) < this.reachDistance;
    }

    public void MoveTowards(Vector3 target) {
        this.direction = new Vector2(target.x, target.y) - new Vector2(this.transform.position.x, this.transform.position.y);
        this.direction.Normalize();

        this.transform.position += new Vector3(this.direction.x, this.direction.y, 0) * Time.deltaTime * this.speed;
    }

    public void ResetDirection() {
        this.direction = new Vector2();
        this.spriteRenderer.sprite = this.sprites[0];
        this.spriteRenderer.flipX = false;
    }

    protected void SetNextState(AIState nextState) {
        if(nextState == this.state) return;
        this.state.Exit();
        this.state = nextState;
        this.state.Enter();
    }

    public override void Update() {
        base.Update();

        this.state.Update();
        this.SetNextState(this.state.NextState());
    }
}
