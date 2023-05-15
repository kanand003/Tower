using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f,5f)] float speed = 1f;
    void Start()
    {
        FindPath();
        ReturnToStart();
        //PrintWaypointName();
        StartCoroutine(PrintWaypointName());
    }

    void FindPath()
    {
        path.Clear();

        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");

        foreach(GameObject Waypoint in waypoints)
        {
            path.Add(Waypoint.GetComponent<Waypoint>());
        }

    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    IEnumerator PrintWaypointName()
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startposition = transform.position;
            Vector3 endposition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endposition);

            while(travelPercent<1f)
            {
                travelPercent += Time.deltaTime*speed;
                transform.position = Vector3.Lerp(startposition, endposition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
            
        }
        Destroy(gameObject);
    }
}
