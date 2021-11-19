using UnityEngine;

public class CutsceneHuman : StateHuman {
    public string textureName = "template";
    public float speed = 0.8f;

    public override State GetBaseState() {
        return new LookAtState(this, Vector2.down);
    }

    protected override string GetTextureName() {
        return textureName;
    }

    public override float GetSpeed() {
        return speed;
    }
}
