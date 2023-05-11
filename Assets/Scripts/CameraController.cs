using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float moveSpeed = 5f;
    private float maxDistance = 150f;
    private float minDistance = 50f;
    private float currentDistance;
    public float rotationSpeed = 50f;

    private void Start()
    {
        currentDistance = Vector3.Distance(transform.position, target.position);

    }


    private void LateUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        currentDistance -= verticalInput * moveSpeed * Time.deltaTime;

        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 newPosition = target.position - direction * currentDistance;

        transform.position = newPosition;

        float rotationInput = Input.GetAxis("Rotation");
        transform.RotateAround(target.position, Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);


    }
}
