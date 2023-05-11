using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowersManager : MonoBehaviour
{
    [SerializeField] int towerLimit;
    [SerializeField] TowerLogic towerPrefab;

    [SerializeField] TextMeshProUGUI towerTextLimit;
    private Queue<TowerLogic> towersQueue = new Queue<TowerLogic>();
    private int towerCount;

    private void Start()
    {
        towerCount = towerLimit;
        towerTextLimit.text = towerCount.ToString();
    }

    public void AddTower(WayPoint wayPoint)
    {
        //towerCount = towersQueue.Count;
        //if (towersQueue.Count < towerLimit)
        if (towerCount > 0)
        {
            CreateTower(wayPoint);
        }
        //else
        //{
        //    MoveTowerToNewPosition(wayPoint);
        //}


    }

    private void CreateTower(WayPoint wayPoint)
    {
        var newTower = Instantiate(towerPrefab, new Vector3(wayPoint.transform.position.x, wayPoint.transform.position.y - 5f, wayPoint.transform.position.z), Quaternion.identity);
        newTower.transform.parent = gameObject.transform;
        wayPoint.SetPartWay(true);
        newTower.baseWaypoint = wayPoint;
        towerCount--;
        towerTextLimit.text = towerCount.ToString();
        //towersQueue.Enqueue(newTower);
    }

    private void MoveTowerToNewPosition(WayPoint newBasePoint)
    {
        TowerLogic oldTower = towersQueue.Dequeue();
        oldTower.transform.position = new Vector3(newBasePoint.transform.position.x, newBasePoint.transform.position.y - 5f, newBasePoint.transform.position.z);
        oldTower.baseWaypoint.SetPartWay(false);
        newBasePoint.SetPartWay(true);
        oldTower.baseWaypoint = newBasePoint;
        towersQueue.Enqueue(oldTower);
    }
}
