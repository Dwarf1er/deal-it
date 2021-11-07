using UnityEngine;

public interface ICutsceneActor {
    void Goto(Vector2 target);

    void SetLookAt(Transform transform);

    void Patrol();

    void Stop();

    bool IsGoto();
    void LookTowards(Vector2 direction);
}