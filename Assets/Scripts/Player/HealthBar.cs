using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
   
    public Image healthBarSprite;
    public GameObject Player;
    [SerializeField, Range(0f, 1f)] private float currentHealth = 1f;

    public float CurrentHealth => currentHealth;

    void Awake()
    {
        if (healthBarSprite == null)
        {
            healthBarSprite = GetComponent<Image>();
        }

        if (healthBarSprite != null)
        {
            healthBarSprite.type = Image.Type.Filled;
        }

        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        UpdateHealthBar(currentHealth);
    }

    private void OnValidate()
    {
        currentHealth = Mathf.Clamp01(currentHealth);

        if (healthBarSprite == null)
        {
            healthBarSprite = GetComponent<Image>();
        }

        if (healthBarSprite != null)
        {
            healthBarSprite.type = Image.Type.Filled;
            healthBarSprite.fillAmount = currentHealth;
        }
    }
   


    public void UpdateHealthBar(float updatedHealth)
    {
        if (healthBarSprite == null)
        {
            return;
        }
    
        currentHealth = Mathf.Clamp01(updatedHealth);
        healthBarSprite.fillAmount = currentHealth;

        if (currentHealth <= 0f && Player != null)
        {
            Destroy(Player);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (healthBarSprite == null)
        {
            return;
        }

        UpdateHealthBar(currentHealth - damageAmount);
    }
}
