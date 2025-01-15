using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;


    public float maxHealth; 
    [HideInInspector] public float currentHealth;
    Ragdoll ragdoll;


    public Slider healthSlider;
    public Image healthFillImage;
    public TextMeshProUGUI hpText;

    void Start()
    {

        currentHealth = 60;

        // Инициализация слайдера и UI

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


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, maxHealth); 


        UpdateUI();

        if (currentHealth <= 0.0f && this.tag == "Enemy")
        {
            ragdoll.ActivateRagdoll();
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, maxHealth);

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
