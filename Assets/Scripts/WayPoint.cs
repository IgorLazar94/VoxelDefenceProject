using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class WayPoint : MonoBehaviour
{
    [HideInInspector] public WayPoint exploredFrom;
    private const int gridSize = 10;
    private bool isChecked = false;
    private bool isPartWay = false;
    public void SetIsChecked(bool value)
    {
        isChecked = value;
    }

    public void SetPartWay(bool value)
    {
        isPartWay = value;
    }

    public bool GetIsChecked()
    {
        return isChecked;
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
                Mathf.RoundToInt(transform.position.x / gridSize),
                Mathf.RoundToInt(transform.position.z / gridSize)
                );
    }

    public void SetTopColor(Color newColor)
    {
        transform.Find("Top").GetComponent<MeshRenderer>().material.color = newColor;
    }

    //private void OnMouseOver()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        print("hello from " + gameObject.name);
    //    }
    //}

    private void OnMouseDown()
    {
        if (!isPartWay)
        {
            FindObjectOfType<TowersManager>().AddTower(this);
        }
    }

}
