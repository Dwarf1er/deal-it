using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractHuman {
    public override void Start() {
        base.Start();

        this.speed = 0.8f;

        EventManager.Get().Subscribe((InputEvent inputEvent) => OnInputEvent(inputEvent));
    }

    private void OnInputEvent(InputEvent inputEvent) {
        if(inputEvent.player != this.transform.name) return;

        this.direction = inputEvent.direction;

        this.transform.position += new Vector3(this.direction.x, this.direction.y, 0) * Time.deltaTime * this.speed;
    }
}
