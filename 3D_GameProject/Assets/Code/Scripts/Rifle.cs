using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rifle : MonoBehaviour
{

    public int maxAmmo = 10;
    public int currentAmmo;
    private float fireRate = 1f;
    public float damage = 10;

    public GameObject CasePrefab;
    public Transform shellEjectPoint;
    private float shellEjectDelay = 0.6f;

    public GameObject explosionPrefab;
    public float explosionRadius = 5f;
    public float explosionDamage = 50;
    private bool isChangingColor = false;

    public Camera playerCamera;
    public TextMeshProUGUI ammoText;
    public ParticleSystem muzzleFlash;
    public GameObject bulletImpactPrefab;

    public Renderer rifleRenderer;
    public int targetMaterialIndex = 3;

    public GameObject regularAmmoIcon;
    public GameObject explosiveAmmoIcon;

    private bool isReloading = false;
    private bool isReadyToFire = true;

    public Animator armsAnimator;
    public Animator gunAnimator;

    public enum AmmoType
    {
        Regular,
        Explosive
    }
    private AmmoType currentAmmoType = AmmoType.Regular;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoDisplay();

    }

    void Update()
    {
        if (isReloading || isChangingColor)
            return;

        if (isReloading)
            return;

        if (Input.GetMouseButtonDown(0) && isReadyToFire)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) || currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchAmmoType();
        }
    }

    void Shoot()
    {
        if (currentAmmo <= 0 || isChangingColor)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            return;
        }

        currentAmmo--;
        isReadyToFire = false;


        UpdateAmmoDisplay();


        if (armsAnimator != null)
        {
            armsAnimator.SetBool("Fire", true);
        }

        if (gunAnimator != null)
        {
            gunAnimator.SetBool("Fire", true);
        }

        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        RaycastHit hit;
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));


        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Object hit - " + hit.collider.name);

            if (currentAmmoType == AmmoType.Regular)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.Log("Enemy hit");
                    MageEnemy enemy = hit.collider.GetComponent<MageEnemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                    }
                }

                var hitBox = hit.collider.GetComponent<HitBox>();
                if (hitBox)
                {
                    hitBox.OnRaycastHit(this, ray.direction);
                }

                CreateBulletImpact(hit);
            }
            else if (currentAmmoType == AmmoType.Explosive)
            {
                CreateExplosion(hit.point);

                StartCoroutine(SetEmissionColor("#202226", "#FF4500", 1.5f));
                isChangingColor = true;
            }
        }
        else if (currentAmmoType == AmmoType.Explosive)
        {

            StartCoroutine(SetEmissionColor("#202226", "#FF4500", 1.5f));
            isChangingColor = true;
        }

        Invoke(nameof(EjectShell), shellEjectDelay);
        Invoke(nameof(ResetFire), 1 / fireRate);

        Invoke(nameof(ResetShootingAnimation), 0.1f);

    }


    void CreateBulletImpact(RaycastHit hit)
    {
        if (bulletImpactPrefab != null)
        {
            GameObject impact = Instantiate(bulletImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(impact, 10f);
        }
    }

    void ResetShootingAnimation()
    {
        if (armsAnimator != null)
        {
            armsAnimator.SetBool("Fire", false);
        }

        if (gunAnimator != null)
        {
            gunAnimator.SetBool("Fire", false);
        }
    }

    void ResetFire()
    {
        isReadyToFire = true;
    }

    IEnumerator Reload()
    {
        isReloading = true;

        if (armsAnimator != null)
        {
            armsAnimator.SetTrigger("OpenBolt");
        }

        if (gunAnimator != null)
        {
            gunAnimator.SetTrigger("OpenBolt");
        }

        yield return new WaitForSeconds(0.5f);

        while (currentAmmo < maxAmmo)
        {
            if (armsAnimator != null)
            {
                armsAnimator.SetTrigger("InsertRound");
            }

            if (gunAnimator != null)
            {
                gunAnimator.SetTrigger("InsertRound");
            }

            yield return new WaitForSeconds(0.80f);

            currentAmmo++;
            UpdateAmmoDisplay();
        }

        if (armsAnimator != null)
        {
            armsAnimator.SetTrigger("CloseBolt");
        }

        if (gunAnimator != null)
        {
            gunAnimator.SetTrigger("CloseBolt");
        }

        yield return new WaitForSeconds(0.5f);

        isReloading = false;
    }


    void UpdateAmmoDisplay()
    {

        if (ammoText != null)
        {
            ammoText.text = $"{currentAmmo} / {maxAmmo}";
        }
    }

    void EjectShell()
    {
        if (CasePrefab != null && shellEjectPoint != null)
        {
            
            GameObject shell = Instantiate(CasePrefab, shellEjectPoint.position, shellEjectPoint.rotation);

            Rigidbody shellRb = shell.GetComponent<Rigidbody>();
            if (shellRb != null)
            {
                
                Vector3 ejectDirection = shellEjectPoint.right + shellEjectPoint.up * 0.5f;
                shellRb.AddForce(ejectDirection * 5f, ForceMode.Impulse);
                shellRb.AddTorque(Random.insideUnitSphere * 50f, ForceMode.Impulse);
            }

            Destroy(shell, 5f);
        }
    }


    void SwitchAmmoType()
    {
        currentAmmoType = currentAmmoType == AmmoType.Regular ? AmmoType.Explosive : AmmoType.Regular;

        if (currentAmmoType == AmmoType.Regular)
        {
            regularAmmoIcon.SetActive(true);
            explosiveAmmoIcon.SetActive(false);

            StartCoroutine(SetEmissionColor("#FF4500", "#202226", 1f));

        }
        else
        {
            regularAmmoIcon.SetActive(false);
            explosiveAmmoIcon.SetActive(true);

            StartCoroutine(SetEmissionColor("#202226", "#FF4500", 1.5f));

        }
    }

    IEnumerator SetEmissionColor(string startHexColor, string targetHexColor, float duration)
    {
        if (rifleRenderer != null)
        {
            Material[] materials = rifleRenderer.materials;
            if (targetMaterialIndex >= 0 && targetMaterialIndex < materials.Length)
            {
                Material targetMaterial = materials[targetMaterialIndex];

                if (ColorUtility.TryParseHtmlString(startHexColor, out Color startColor) &&
                    ColorUtility.TryParseHtmlString(targetHexColor, out Color targetColor))
                {
                    targetMaterial.EnableKeyword("_EMISSION");

                    float time = 0f;
                    while (time < duration)
                    {
                        time += Time.deltaTime;
                        Color lerpedColor = Color.Lerp(startColor, targetColor, time / duration);
                        targetMaterial.SetColor("_EmissionColor", lerpedColor);
                        yield return null;
                    }

                    targetMaterial.SetColor("_EmissionColor", targetColor);
                }
                else
                {
                    Debug.LogError($"Invalid HEX color: {startHexColor} or {targetHexColor}");
                }
            }
            else
            {
                Debug.LogError($"Material index {targetMaterialIndex} is out of range.");
            }
        }
        else
        {
            Debug.LogError("Rifle renderer is not assigned.");
        }

        isChangingColor = false;
    }

    void CreateExplosion(Vector3 explosionPoint)
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, explosionPoint, Quaternion.identity);
            Destroy(explosion, 10f);
        }

        Collider[] colliders = Physics.OverlapSphere(explosionPoint, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Enemy"))
            {
                MageEnemy enemy = nearbyObject.GetComponent<MageEnemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(explosionDamage);
                }
            }

            if (nearbyObject.CompareTag("Player"))
            {
                Health playerHealth = nearbyObject.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(explosionDamage);
                }
            }

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(700f, explosionPoint, explosionRadius);
            }
        }
    }
}
