using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{   
    List<Node> path = new();
    [SerializeField] [Range(0f,5f)] float speed = 1f;

    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;

    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }
    void Awake()
    {
        enemy = GetComponent<Enemy>();
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
    }
    void RecalculatePath( bool resetPath)
    {
        Vector2Int coordinates = new();
        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path=pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
        //GameObject pathGo = GameObject.FindGameObjectWithTag("Path");

        //foreach (Transform tile in pathGo.transform)
        //{
        //    Tile waypoint = tile.GetComponent<Tile>();

        //    if (waypoint != null)
        //    {
        //        path.Add(waypoint);
        //    }
        //}
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        for(int i=1;i< path.Count;i++)
        {
            Vector3 startposition = transform.position;
            Vector3 endposition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endposition);

            while(travelPercent<1f)
            {
                travelPercent += Time.deltaTime*speed;
                transform.position = Vector3.Lerp(startposition, endposition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }
}
