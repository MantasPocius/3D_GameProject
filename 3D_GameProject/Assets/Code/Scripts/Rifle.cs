using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rifle : MonoBehaviour
{

    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 2f;
    public float fireRate = 1f;

    public GameObject CasePrefab;
    public Transform shellEjectPoint;
    public float shellEjectDelay = 0.5f;

    public Camera playerCamera;
    public TextMeshProUGUI ammoText;
    public ParticleSystem muzzleFlash;

    private bool isReloading = false;
    private bool isReadyToFire = true;


    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoDisplay();
    }

    void Update()
    {
        if (isReloading)
            return;

        if (Input.GetButtonDown("Fire1") && isReadyToFire)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) || currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }

    }

    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("No bullet");
            return;
        }

        currentAmmo--;
        isReadyToFire = false;


        UpdateAmmoDisplay();

        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        RaycastHit hit;
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));


        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Object hit - " + hit.collider.name);


            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy hit");
            }
        }

        Invoke(nameof(EjectShell), shellEjectDelay);
        Invoke(nameof(ResetFire), 1 / fireRate);
    }

    void ResetFire()
    {
        isReadyToFire = true;
    }

    IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Reload");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo; 
        isReloading = false;


        Debug.Log("Reload complete");
        UpdateAmmoDisplay();
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
