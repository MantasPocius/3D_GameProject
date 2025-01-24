using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]

public class AiAgentConfig : ScriptableObject
{
    public float maxTime = 1.0f;
    public float maxDistance = 20.0f;
    public float dieForce = 5.0f;
    public float maxSightDistance = 20.0f;
}
