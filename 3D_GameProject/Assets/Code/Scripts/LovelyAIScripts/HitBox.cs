using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public EnemyHealth health;
    public void OnRaycastHit(Rifle weapon, Vector3 direction)
    {
        health.TakeDamage(weapon.damage, direction);
    }

    public void OnRaycastHit(SMG weapon, Vector3 direction)
    {
        health.TakeDamage(weapon.damage, direction);
    }
}
