using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    public float maxHealth; 
    [HideInInspector] public float currentHealth;

    public Slider healthSlider;
    public Image healthFillImage;
    public TextMeshProUGUI hpText;

    public TextMeshProUGUI deathText;
    public bool isDead = false;

    private Movement Movement;

    void Start()
    {
        currentHealth = maxHealth;
       
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        if (healthFillImage != null)
        {
            healthFillImage.color = Color.white;
        }

        if (deathText != null)
        {
            deathText.gameObject.SetActive(false);
        }

        Movement = GetComponent<Movement>();
    }


    private void Update()
    {
        if (isDead)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Heal(10);
        }

    }


    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, maxHealth); 

        UpdateHealthBar();
        UpdateHpDisplay();
        UpdateHealthColor();
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (deathText != null)
        {
            isDead = true;

            deathText.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        if (Movement != null)
        {
            Movement.enabled = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeathBox"))
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        if (isDead) return;

        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, maxHealth);

        UpdateHealthBar();
        UpdateHpDisplay();
        UpdateHealthColor();
    }

  
    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;
    }

    void UpdateHpDisplay()
    {

        if (hpText != null)
        {
            hpText.text = $"{currentHealth}";
        }
    }

    private void UpdateHealthColor()
    {
        if (healthFillImage != null)
        {

            if (currentHealth <= 20)
            {
                healthFillImage.color = Color.red;
            }
            else
            {
                healthFillImage.color = Color.white;
            }
        }
    }

}