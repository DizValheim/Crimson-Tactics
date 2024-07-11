using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Handles Enemy Movement</summary>
public class EnemyAI : MonoBehaviour, IAI
{

    [Tooltip("How fast the Enemy moves.")]
    public float movementSpeed = 1f;    

    Vector2Int targetCoords;
    bool isMoving;

    GridManager gridManager;
    Pathfinding pathfinder;

    List<Node> path;
    Transform unitTransform;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinding>();
        unitTransform = transform;
    }

    /// <summary>Updates target according to player unit's position and calls for Movement method.</summary>
    public void UpdateTarget(Vector2Int newTargetCoords)
    {
        gridManager.Grid[gridManager.GetCoordinatesFromPosition(unitTransform.position)].walkable = true;
        targetCoords = newTargetCoords;
        isMoving = true;
        path = null;
        MoveTowardsTarget();
        
    }

    /// <summary>Handles movement of the Enemy towards Player Unit</summary>
    public void MoveTowardsTarget()
    {

        if (isMoving && targetCoords != Vector2Int.zero)
        {
            Vector2Int unitCoords = gridManager.GetCoordinatesFromPosition(unitTransform.position);
            pathfinder.SetNewDestination(unitCoords, targetCoords);
            StopAllCoroutines();
            path?.Clear();
            path = pathfinder.GetNewPath(unitCoords);
            StartCoroutine(FollowPath());

        }
    }

    IEnumerator FollowPath()
    {

        for (int i = 0; i < path.Count-1; i++)
        {
            Vector3 startPosition = unitTransform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coords);
            if (startPosition == endPosition)
                continue;
            float travelPercent = 0f;

            unitTransform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * movementSpeed;
                unitTransform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }

            
        }
        gridManager.Grid[gridManager.GetCoordinatesFromPosition(unitTransform.position)].walkable = false;

    }

}
