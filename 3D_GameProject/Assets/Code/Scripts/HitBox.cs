using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Health health;
    public void OnRaycastHit(Rifle weapon)
    {
        health.TakeDamage(weapon.damage);
    }
}
