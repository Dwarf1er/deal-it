using UnityEngine;

public interface IStateHandler : IWithTransform {
    void MoveTowards(Vector2 position);
    void ResetDirection();
    float DistanceTo(Vector2 position);
    void LookAt(Vector2 position);
    void LookTowards(Vector2 direction);
    TilemapGraph GetGraph();
    void SetDirection(Vector2 direction);
    Vector2 GetDirection();
    State GetBaseState();
}
