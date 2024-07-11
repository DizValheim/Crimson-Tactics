using UnityEngine;

public interface IAI
{
    void UpdateTarget(Vector2Int targetCoords);
    void MoveTowardsTarget();
}