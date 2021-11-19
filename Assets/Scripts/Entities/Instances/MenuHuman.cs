using UnityEngine;
using System;

class MenuHuman : StateHuman {

    public string textureName;

    public override State GetBaseState() {
        return new LookAtState(this, Vector2.down);
    }

    public override float GetSpeed() {
        return 1;
    }

    protected override string GetTextureName() {
        return textureName;
    }
}
