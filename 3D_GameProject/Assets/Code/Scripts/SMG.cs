using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SMG : MonoBehaviour
{
    public int maxAmmo = 30;
    public int currentAmmo;
    public float fireRate = 10f;
    public float damage = 10;

    public GameObject CasePrefab;
    public Transform shellEjectPoint;
    public float shellEjectDelay = 0.1f;

    public Camera playerCamera;
    public TextMeshProUGUI ammoText;
    public ParticleSystem muzzleFlash;
    public GameObject bulletImpactPrefab;

    public bool isReloading = false;
    private bool isReadyToFire = true;

    private float nextFireTime = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoDisplay();
    }

    void Update()
    {
        if (isReloading)
            return;

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && isReadyToFire)
        {
            nextFireTime = Time.time + 1f / fireRate;
            Shoot();
        }

        if ((Input.GetKeyDown(KeyCode.R) || currentAmmo <= 0) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            return;
        }

        currentAmmo--;
        UpdateAmmoDisplay();

        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        RaycastHit hit;
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    Vector3 direction = hit.point - transform.position;
                    enemyHealth.TakeDamage(damage, direction.normalized);
                }
            }

            var hitBox = hit.collider.GetComponent<HitBox>();
            if (hitBox)
            {
                hitBox.OnRaycastHit(this, ray.direction);
            }

            CreateBulletImpact(hit);
        }

        Invoke(nameof(EjectShell), shellEjectDelay);
    }

    void CreateBulletImpact(RaycastHit hit)
    {
        if (bulletImpactPrefab != null)
        {
            GameObject impact = Instantiate(bulletImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 10f);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        isReadyToFire = false;

        yield return new WaitForSeconds(3f);

        currentAmmo = maxAmmo;
        UpdateAmmoDisplay();

        isReloading = false;
        isReadyToFire = true;
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
}
