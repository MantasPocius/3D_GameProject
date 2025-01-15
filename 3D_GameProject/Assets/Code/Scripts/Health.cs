using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Slider healthSlider;
    public Image healthFillImage;
    public TextMeshProUGUI hpText;

    void Start()
    {
        currentHealth = 60;

        // Инициализация слайдера и UI
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        if (healthFillImage != null)
        {
            healthFillImage.color = Color.white;
        }
        UpdateHpDisplay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Heal(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateUI();

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateUI();
    }

    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateUI();
    }

    private void UpdateUI()
    {
        UpdateHealthBar();
        UpdateHpDisplay();
        UpdateHealthColor();
    }

    private void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    private void UpdateHpDisplay()
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
            healthFillImage.color = currentHealth <= 20 ? Color.red : Color.white;
        }
    }
}
