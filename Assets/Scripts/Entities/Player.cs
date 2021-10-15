using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractHuman {
    public float speed = 1.0f;
    private Controller controller;

    public override void Start() {
        base.Start();
        this.controller = this.GetComponent<Controller>();
    }

    public override void Update() {
        this.direction = new Vector2(this.controller.GetHorizontal(), this.controller.GetVertical()).normalized;

        this.transform.position += new Vector3(this.direction.x, this.direction.y, 0) * Time.deltaTime * this.speed;

        base.Update();
    }
}
