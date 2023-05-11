using System.Collections;
using TMPro;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] [Range(1f, 20f)] float spawnEnemyInterval;
    [SerializeField] EnemyMovement enemyPrefab;

    [SerializeField] AudioClip enemySpawnSoundFX;
    [SerializeField] private int enemyLimit;
    [SerializeField] private TextMeshProUGUI EnemiesLeftText;
    [HideInInspector] public int lastEnemies;
    private AudioSource audioSource;
    private void Start()
    {
        lastEnemies = enemyLimit;
        EnemiesLeftText.text = lastEnemies.ToString();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartEnemySpawn());
    }

    private IEnumerator StartEnemySpawn()
    {
        while (lastEnemies > 0)
        {
            audioSource.PlayOneShot(enemySpawnSoundFX);
            var newEnemy = Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
            newEnemy.transform.parent = gameObject.transform;
            lastEnemies--;
            EnemiesLeftText.text = lastEnemies.ToString();
            yield return new WaitForSeconds(spawnEnemyInterval);
        }
    }
}
