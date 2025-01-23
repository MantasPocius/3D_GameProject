using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIk : MonoBehaviour
{
    public Transform targetTransform;
    public Transform aimTransform;
    public Transform bone;

    public int iterations = 10;
    [Range(0,1)]
    public float weight = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = targetTransform.position;
        for(int i = 0; i < iterations; i++)
        {
            AimAtTarget(bone, targetPosition, weight);
        }
        
    }

    private void AimAtTarget(Transform bone, Vector3 targetPosition, float weight)
    {
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirection = targetPosition - aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
        bone.rotation = blendedRotation * bone.rotation;
    }
}
