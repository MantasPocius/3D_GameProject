using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthTest : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Slider healthSlider;
    public Image healthFillImage;
    public TextMeshProUGUI hpText;

    void Start()
    {
        currentHealth = 60;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        UpdateUI();
    }

    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
        if (hpText != null)
        {
            hpText.text = $"{currentHealth}";
        }
        if (healthFillImage != null)
        {
            healthFillImage.color = currentHealth <= 20 ? Color.red : Color.white;
        }
    }
}
