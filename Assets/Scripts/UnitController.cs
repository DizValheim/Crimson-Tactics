using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Handles Player controls</summary>
public class UnitController : MonoBehaviour
{
    public float movementSpeed = 1f;

    Transform selectedUnit;
    bool unitSelected = false;

    List<Node> path = new List<Node>();

    GridManager gridManager;
    Pathfinding pathfinder;

    EnemyAI[] enemies;

    void Start() 
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinding>();
        enemies = FindObjectsOfType<EnemyAI>();
    }

    void Update() 
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);

            if(hasHit)
            {
                if(hit.transform.tag == "Tile" && selectedUnit != null)
                {
                    Vector2Int targetCoords = hit.transform.GetComponent<Tile>().coords;
                    Vector2Int startCoords = new Vector2Int((int)selectedUnit.position.x, (int)selectedUnit.position.z) / gridManager.UnityGridSize;
                    pathfinder.SetNewDestination(startCoords, targetCoords);
                    RecalculatePath(true);
                }

                if(hit.transform.tag == "Unit")
                {
                    selectedUnit = hit.transform;
                    unitSelected = true;
                }
            }
        }    
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if(resetPath)
        {
            coordinates = pathfinder.StartCoords;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());

    }

    IEnumerator FollowPath()
    {
        for (int i = 0; i < path.Count; i++)
        {
            Vector3 startPosition = selectedUnit.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coords);
            float travelPercent = 0f;
            
            selectedUnit.LookAt(endPosition);

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * movementSpeed;
                // Debug.Log(travelPercent);
                selectedUnit.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        if(enemies.Length > 0)
        {
            foreach (EnemyAI enemy in enemies)
            {
                enemy.UpdateTarget(gridManager.GetCoordinatesFromPosition(selectedUnit.position));
                // Debug.Log($"Updated position at ");
            }
        }
    }
}
