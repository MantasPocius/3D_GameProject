using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rifle;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject[] weaponIcons;

    private int currentWeaponIndex = 0;

    void Start()
    {
        EquipWeapon(0);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1);
        }
    }

    void EquipWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weapons.Length)
            return;

        for (int i = 0; i < weapons.Length; i++)
        {
            bool isActive = i == weaponIndex;

            weapons[i].SetActive(isActive);

            Rifle rifle = weapons[i].GetComponent<Rifle>();
            if (rifle != null)
            {
                if (isActive)
                {
                    rifle.ResetEmissionColor();
                    rifle.SetAmmoIconsActive(true);

                    if (rifle.isReloading)
                    {
                        rifle.StopCoroutine(rifle.Reload());
                        rifle.isReloading = false;
                    }

                    if (!rifle.isReadyToFire)
                    {
                        rifle.isReadyToFire = true;
                    }


                    rifle.StopColorChange();
                    rifle.isChangingColor = false;
                }
                else
                {
                    rifle.SetAmmoIconsActive(false);
                }
            }
        }
        currentWeaponIndex = weaponIndex;

        UpdateWeaponIcons(weaponIndex);

        UpdateWeaponIcons(weaponIndex);
    }

    void UpdateWeaponIcons(int weaponIndex)
    {
        for (int i = 0; i < weaponIcons.Length; i++)
        {
            weaponIcons[i].SetActive(i == weaponIndex);
        }
    }
}
