using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private WayPoint startPoint;
    [SerializeField] private WayPoint finishPoint;
    Dictionary<Vector2Int, WayPoint> gridDictionary = new Dictionary<Vector2Int, WayPoint>();
    Queue<WayPoint> queueWayPoints = new Queue<WayPoint>();
    private bool isRunning = true;
    private List<WayPoint> path = new List<WayPoint>();
    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };
    [SerializeField] private Material wayMaterial;

    private WayPoint searchPoint;

    public List<WayPoint> GetPathWay()
    {
        if (path.Count == 0)
        {
            CalculatePath();
        }
            return path;
    }

    private void CalculatePath()
    {
        LoadBlocks();
        SetSpecialWaypointColor();
        PathFindAlgorithm();
        CreatePath();
    }

    private void CreatePath()
    {
        AddPointToPath(finishPoint);
        WayPoint prevPoint = finishPoint.exploredFrom;
        while (prevPoint != startPoint)
        {
            //prevPoint.SetTopColor(Color.blue);
            prevPoint.transform.GetChild(1).GetComponent<MeshRenderer>().material = wayMaterial;
            AddPointToPath(prevPoint);
            prevPoint = prevPoint.exploredFrom;
        }
        AddPointToPath(startPoint);
        path.Reverse();
    }

    private void AddPointToPath(WayPoint wayPoint)
    {
        path.Add(wayPoint);
        wayPoint.SetPartWay(true);
    }

    private void PathFindAlgorithm()
    {
        queueWayPoints.Enqueue(startPoint);
        while (queueWayPoints.Count > 0 && isRunning)
        {
            searchPoint = queueWayPoints.Dequeue();
            searchPoint.SetIsChecked(true);
            CheckFinishPoint();
            ExploreNearPoints();
        }


    }

    private void CheckFinishPoint()
    {
        if (searchPoint == finishPoint)
        {
            Debug.Log("Find Finish at point " + searchPoint);
            searchPoint.SetTopColor(Color.red);
            isRunning = false;
        }
    }

    private void ExploreNearPoints()
    {
        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int nearPointCoordinates = searchPoint.GetGridPos() + direction;
            try
            {
                WayPoint nearPoint = gridDictionary[nearPointCoordinates];

                AddPointToQueue(nearPoint);
                //nearPoint.SetTopColor(Color.yellow);
            }
            catch
            {
                //Debug.LogWarning("Neighbor wayPoint with coordinates:" + nearPointCoordinates + " - not found");
            }
        }
    }

    private void AddPointToQueue(WayPoint nearPoint)
    {
        if (nearPoint.GetIsChecked() || queueWayPoints.Contains(nearPoint))
        {
            return;
        }
        else
        {
            queueWayPoints.Enqueue(nearPoint);
            nearPoint.exploredFrom = searchPoint;
        }
    }

    private void LoadBlocks()
    {
        var wayPoints = GetComponentsInChildren<WayPoint>();

        foreach (WayPoint wayPoint in wayPoints)
        {
            Vector2Int gridPos = wayPoint.GetGridPos();
            if (!gridDictionary.ContainsKey(gridPos))
            {
                gridDictionary.Add(gridPos, wayPoint);
            }
            else
            {
                Debug.LogWarning("WayPoint Block has dublicate :" + wayPoint);
            }
        }

    }

    private void SetSpecialWaypointColor()
    {
        //startPoint.SetTopColor(Color.green);
        //finishPoint.SetTopColor(Color.red);
        startPoint.transform.GetChild(1).GetComponent<MeshRenderer>().material = wayMaterial;
        finishPoint.transform.GetChild(1).GetComponent<MeshRenderer>().material = wayMaterial;
    }
}
