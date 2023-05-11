using TMPro;
using UnityEngine;

[SelectionBase]
public class EnemyDamage : MonoBehaviour
{
    private int healthPoints = 10;
    [SerializeField] private ParticleSystem hitFX;
    [SerializeField] private ParticleSystem deathFX;
    [SerializeField] private AudioClip hitSoundFX;
    [SerializeField] private AudioClip deathSoundFX;
    private TextMeshProUGUI scoresText;
    private int currentScores;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        scoresText = GameObject.Find("Scores").GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnParticleCollision(GameObject other)
    {
        GetDamage();
    }

    private void GetDamage()
    {
        healthPoints = healthPoints - 1;
        hitFX.Play();
        audioSource.PlayOneShot(hitSoundFX);
        if (healthPoints <= 0)
        {
            DestroyEnemy(deathFX, true);
        }
    }

    public void DestroyEnemy(ParticleSystem particles, bool addScores)
    {
        if (addScores)
        {
            UpdateScores();
        }
        ParticleSystem deathParticles = Instantiate(particles, transform.position, Quaternion.identity);
        deathParticles.Play();

        AudioSource.PlayClipAtPoint(deathSoundFX, Camera.main.gameObject.transform.position);

        Destroy(deathParticles.gameObject, deathParticles.main.duration);
        Destroy(gameObject);
        FindObjectOfType<GameManager>().GetComponent<GameManager>().CheckEnemies();
    }

    private void UpdateScores()
    {
        currentScores = int.Parse(scoresText.text);
        currentScores += 5;
        scoresText.text = currentScores.ToString();
    }
}
