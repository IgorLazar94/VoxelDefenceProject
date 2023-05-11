using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private ParticleSystem castleDamageFX;
    [SerializeField] private float timeToStep = 1.0f;
    [SerializeField] private float moveStep = 1.0f;
    CastleLogic castleLogic;
    private PathFinder pathFinder;
    private EnemyDamage enemyDamage;
    private Vector3 targetPos;

    void Start()
    {
        castleLogic = FindObjectOfType<CastleLogic>();
        enemyDamage = gameObject.GetComponent<EnemyDamage>();
        pathFinder = FindObjectOfType<PathFinder>();
        StartCoroutine(MoveToWayPoint());
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveStep);
    }

    private IEnumerator MoveToWayPoint()
    {
        foreach (var wayPoint in pathFinder.GetPathWay())
        {
            gameObject.transform.LookAt(wayPoint.transform);
            targetPos = wayPoint.transform.position;
            yield return new WaitForSeconds(timeToStep);
        }
        castleLogic.TakeDamage();
        enemyDamage.DestroyEnemy(castleDamageFX, false);
    }
}