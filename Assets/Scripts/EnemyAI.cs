using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAI
{

    public float movementSpeed = 1f;

    [SerializeField] Vector2Int targetCoords;
    bool isMoving;

    GridManager gridManager;
    Pathfinding pathfinder;

    List<Node> path;
    int currentPathIndex;
    Transform unitTransform;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinding>();
        unitTransform = transform;
    }

    public void UpdateTarget(Vector2Int newTargetCoords)
    {
        targetCoords = newTargetCoords;
        isMoving = true;
        currentPathIndex = 0;
        path = null;
        MoveTowardsTarget();
    }

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
            // Debug.Log(path[i].coords);
            float travelPercent = 0f;

            unitTransform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * movementSpeed;
                unitTransform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }

            
        }

    }

}
