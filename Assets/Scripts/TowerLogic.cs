using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class TowerLogic : MonoBehaviour
{
    [SerializeField] Transform towerHead;
    [SerializeField] private Transform targetEnemy;
    private ParticleSystem bulletsFX;
    private float shootRange = 30f;
    public WayPoint baseWaypoint;
    private void Start()
    {
        bulletsFX = GetComponentInChildren<ParticleSystem>();
    }
    void Update()
    {
        ChooseTargetEnemy();
        if (targetEnemy != null)
        {
            towerHead.LookAt(targetEnemy.transform);
            CheckDistance();
        }
        else
        {
            Shoot(false);
        }
    }

    private void ChooseTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) { return; }
        
        Transform nearestEnemy = sceneEnemies[0].transform;

        foreach (EnemyDamage enemy in sceneEnemies)
        {
            nearestEnemy = GetClosestEnemy(nearestEnemy.transform, enemy.transform);
        }
        targetEnemy = nearestEnemy;
    }

    private Transform GetClosestEnemy(Transform enemy_APos, Transform enemy_BPos)
    {
        var distToA = Vector3.Distance(enemy_APos.position, transform.position);
        var distToB = Vector3.Distance(enemy_BPos.position, transform.position);
        if (distToA < distToB)
        {
            return enemy_APos;
        }
        else
        {
            return enemy_BPos;
        }
    }

    private void CheckDistance()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if (distanceToEnemy <= shootRange)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        ParticleSystem.EmissionModule enableFX = bulletsFX.emission;
        enableFX.enabled = isActive;
    }

}
