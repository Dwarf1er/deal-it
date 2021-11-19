using UnityEngine;

public interface IStateHandler : IWithTransform, IWithPosition {
    void MoveTowards(Vector2 position);
    void ResetDirection();
    float DistanceTo(Vector2 position);
    void LookAt(Vector2 position);
    void LookTowards(Vector2 direction);
    Vector2[] GetPath(Vector2 from, Vector2 to);
    Vector2 RandomPosition();
    void SetDirection(Vector2 direction);
    Vector2 GetDirection();
    State GetBaseState();
    float GetSpeed();
}
