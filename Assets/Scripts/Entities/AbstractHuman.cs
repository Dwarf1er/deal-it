using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractHuman : MonoBehaviour, ISubscriber {
    public float speed = 1.0f;
    public Sprite[] sprites;
    public Vector2 direction;
    private int spriteIndex;
    protected SpriteRenderer spriteRenderer;

    public virtual void Start() {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    public bool HasDistance() {
        return true;
    }

    public Transform GetTransform() {
        return this.transform;
    }

    private int GetDirectionIndex() {
        if(direction.magnitude == 0) return -1;

        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            return direction.x > 0 ? 2 : 3;
        } else {
            return direction.y < 0 ? 0 : 1;
        }
    }

    public float DistanceTo(GameObject gameObject) {
        return this.DistanceTo(gameObject.transform);
    }

    public float DistanceTo(Transform transform) {
        return this.DistanceTo(transform.position);
    }

    public float DistanceTo(Vector2 target) {
        return this.DistanceTo(new Vector3(target.x, target.y, 0));
    }

    public float DistanceTo(Vector3 target) {
        return Vector2.Distance(this.transform.position, target);
    }

    public virtual void Update() {
        int directionIndex = this.GetDirectionIndex();

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

        if(Time.frameCount % 10 == 0) {
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
        }

        if(this.spriteIndex >= 0) this.spriteRenderer.sprite = this.sprites[this.spriteIndex];
    }
}
