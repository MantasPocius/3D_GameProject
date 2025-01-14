using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public float maxHealth; 
    [HideInInspector] public float currentHealth;
    Ragdoll ragdoll;

    public Slider healthSlider;
    public Image healthFillImage;
    public TextMeshProUGUI hpText;


    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();

        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach(var rigidbody in rigidbodies)
        {
            HitBox hitbox = rigidbody.gameObject.AddComponent<HitBox>();
            hitbox.health = this;
        }
        currentHealth = maxHealth;
       
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        if (healthFillImage != null)
        {
            healthFillImage.color = Color.white;
        }

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


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, maxHealth); 

        UpdateHealthBar();
        UpdateHpDisplay();
        UpdateHealthColor();

        if (currentHealth <= 0.0f && this.tag == "Enemy")
        {
            ragdoll.ActivateRagdoll();
        }
    }

    public void Heal(float healAmount)
    {
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