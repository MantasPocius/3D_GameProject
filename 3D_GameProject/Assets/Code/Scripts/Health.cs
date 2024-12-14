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
  
        currentHealth = maxHealth;
       
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        if (healthFillImage != null)
        {
            healthFillImage.color = Color.white;
        }

    }

   
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 

        UpdateHealthBar();
        UpdateHpDisplay();
        UpdateHealthColor();

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }

    // Метод для исцеления
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

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