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

    public Camera playerCamera;

    public TextMeshProUGUI ammoText;

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

        if (Input.GetKeyDown(KeyCode.R))
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



        Debug.Log("Shot, bullet left " + currentAmmo);
        UpdateAmmoDisplay();


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
        else
        {
            Debug.Log("no hit");
        }

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



}
