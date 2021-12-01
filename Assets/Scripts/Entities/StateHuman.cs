using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TilemapNavAgent))]
public abstract class StateHuman : MonoBehaviour, ISubscriber, IWithPosition, IWithTransform, IStateHandler, ICutsceneActor {
    private Vector2 direction;
    private Sprite[] sprites;
    private int spriteIndex;
    private float flipTime;
    private SpriteRenderer spriteRenderer;
    private State state;
    private TilemapNavAgent agent;

    protected virtual void Start() {
        UpdateTextures();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.agent = GetComponent<TilemapNavAgent>();
        this.state = GetBaseState();
        this.state.Enter();
    }

    public void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    public abstract State GetBaseState();
    public abstract float GetSpeed();

    protected abstract string GetTextureName();

    protected void UpdateTextures() {
        this.sprites = Resources.LoadAll<Sprite>("Characters/" + GetTextureName());
    }

    public Vector2 GetPosition() {
        return this.transform.position;
    }

    public Vector2[] GetPath(Vector2 from, Vector2 to) {
        return agent.GetPath(from, to);
    }

    public Vector2 RandomPosition() {
        return agent.RandomPosition();
    }

    public State GetState() {
        return state;
    }

    public void SetDirection(Vector2 direction) {
        this.direction = direction;
    }

    public Vector2 GetDirection() {
        return direction;
    }

    public bool HasDistance() {
        return true;
    }

    public Transform GetTransform() {
        return this.transform;
    }

    public float DistanceTo(Vector2 target) {
        return Vector2.Distance(this.transform.position, target);
    }

    private int GetDirectionIndex(Vector2 direction) {
        if(direction.magnitude == 0) return -1;

        float angle = Vector2.SignedAngle(Vector2.right, direction) + 180.0f;

        if(angle >= 45 && angle < 135) {
            return 0;
        } else if(angle >= 135 && angle < 225) {
            return 2;
        } else if(angle >= 225 && angle < 315) {
            return 1;
        } else {
            return 3;
        }
    }

    public void LookAt(Vector2 position) {
        Vector2 displacement = position - (Vector2)this.transform.position;

        if(displacement.magnitude == 0) return;

        LookTowards(displacement.normalized);
    }

    public void LookTowards(Vector2 direction) {
        if(direction.magnitude == 0) return;

        int directionIndex = this.GetDirectionIndex(direction);

        switch(directionIndex) {
            case 0:
            case 1:
                this.spriteIndex = directionIndex;
                break;
            case 2:
            case 3:
                if(this.spriteIndex < 2) this.spriteIndex = 3;
                this.spriteRenderer.flipX = directionIndex == 2;
                break;
        }
    }

    public virtual void Update() {
        LookTowards(direction);

        int directionIndex = this.GetDirectionIndex(direction);

        flipTime += Time.deltaTime;

        if (flipTime > 0.175f) {
            switch(directionIndex) {
                case 0:
                case 1:
                    this.spriteRenderer.flipX = !this.spriteRenderer.flipX;
                    break;
                case 2:
                case 3:
                    this.spriteIndex = this.spriteIndex == 3 ? 2 : 3;
                    break;
            }
            flipTime = 0;
        }

        if(this.spriteIndex >= 0) this.spriteRenderer.sprite = this.sprites[this.spriteIndex];

        this.state.Loop();
        this.SetNextState(this.state.NextState());
    }

    public void MoveTowards(Vector2 target) {
        this.direction = (Vector2)target - (Vector2)this.transform.position;
        this.direction.Normalize();

        this.transform.position += (Vector3)(direction * Time.deltaTime * GetSpeed());
    }

    public void ResetDirection() {
        this.direction = Vector2.zero;
    }

    protected void SetNextState(State nextState) {
        if(nextState == this.state) return;

        this.state.Exit();
        this.state = nextState;
        this.state.Enter();
    }

    /// ICutsceneActor
    public void Goto(Vector2 target) {
        SetNextState(new GotoState(this, target));
    }

    public void SetLookAt(Transform transform) {
        SetNextState(new LookAtState(this, transform));
    }

    public void Patrol() {
        SetNextState(new PatrolState(this));
    }

    public void Stop() {
        SetNextState(new LookAtState(this, Vector2.zero));
    }

    public bool IsGoto() {
        return state is GotoState;
    }
}
