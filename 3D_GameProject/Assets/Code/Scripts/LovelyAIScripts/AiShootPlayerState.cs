using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rifle;

public class AiShootPlayerState : AiState
{
    public void Enter(AiAgent agent)
    {
        if (agent.currentAmmo <= 0)
        {
            return;
        }


        agent.currentAmmo--;
        agent.isReadyToFire = false;

        if (agent.muzzleFlash != null)
        {
            agent.muzzleFlash.Play();
        }

        /*
        if (Physics.Raycast(agent, out hit))
        {

            if (currentAmmoType == AmmoType.Regular)
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
            else if (currentAmmoType == AmmoType.Explosive)
            {
                CreateExplosion(hit.point);

                StartCoroutine(SetEmissionColor("#202226", "#FF4500", 1.5f, true));
                isChangingColor = true;
            }
        }
        else if (currentAmmoType == AmmoType.Explosive)
        {

            StartCoroutine(SetEmissionColor("#202226", "#FF4500", 1.5f, true));
            isChangingColor = true;
        }

        Invoke(nameof(EjectShell), shellEjectDelay);
        Invoke(nameof(ResetFire), 1 / fireRate);

        Invoke(nameof(ResetShootingAnimation), 0.1f);*/
    }

    public void Exit(AiAgent agent)
    {
        
    }

    public AiStateId GetId()
    {
        return AiStateId.ShootPlayer;
    }

    public void Update(AiAgent agent)
    {
        
    }
}
