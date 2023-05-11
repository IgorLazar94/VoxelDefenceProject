using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[ExecuteInEditMode]
[RequireComponent(typeof(WayPoint))]
public class CubeEditor : MonoBehaviour
{
    WayPoint wayPoint;

    private void Awake()
    {
        wayPoint = gameObject.GetComponent<WayPoint>();
    }

    void Update()
    {
        UpdatePosition();
        //SetLabelText();
    }

    private void UpdatePosition()
    {
        int gridSize = wayPoint.GetGridSize();
        transform.position = new Vector3(wayPoint.GetGridPos().x * gridSize, 0f, wayPoint.GetGridPos().y * gridSize);
    }

    private void SetLabelText()
    {
        TextMesh TextMeshlabel = GetComponentInChildren<TextMesh>();
        string labelName = "X:" + wayPoint.GetGridPos().x + ",\nZ:" + wayPoint.GetGridPos().y;
        TextMeshlabel.text = labelName;
        SetGameObjectName();
    }

    private void SetGameObjectName()
    {
        gameObject.name = "X:" + wayPoint.GetGridPos().x + ", Z:" + wayPoint.GetGridPos().y;
    }

}
