using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CastleLogic : MonoBehaviour
{
    public int castleHP;
    [SerializeField] private AudioClip audioCastleDamageFX;
    [SerializeField] Image castleHPFull;
    [SerializeField] GameManager gameManager;
    private int damage = 10;
    private AudioSource audioSource;
    private float coefficientDamage;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        CalculateDamageForHPBar();
        //castleHPText.text = castleHP.ToString();    
    }

    public void TakeDamage()
    {
        castleHP -= damage;
        UpdateHealthBar();
        //castleHPText.text = castleHP.ToString();
        audioSource.PlayOneShot(audioCastleDamageFX);
        if (castleHP <= 0)
        {
            PlayerLose();
        }
    }

    private void PlayerLose()
    {
            gameManager.LoseGame();
    }

    private void CalculateDamageForHPBar()
    {
        coefficientDamage = castleHPFull.fillAmount / castleHP;
    }

    private void UpdateHealthBar()
    {
        castleHPFull.fillAmount -= coefficientDamage * damage;
    }
}
