using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class LightChange : MonoBehaviour
{
    [SerializeField] private Light Light;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientIntensity = 1;
        Light.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        StartCoroutine(Intensity(other));
    }

    IEnumerator Intensity(Collider other)
    {
        if (other.tag == "Lightness" && RenderSettings.ambientIntensity == 0f)
        {
            Debug.Log("Light");
            for (int i = 0; i <= 100; i += 1)
            {
                RenderSettings.ambientIntensity = (float)i / 100;
                Light.intensity = (float)i / 100;
                yield return new WaitForSeconds(0.01f);
            }
        }
        if (other.tag == "DAHKNESS" && RenderSettings.ambientIntensity == 1f)
        {
            Debug.Log("Dark");
            for (int i = 100; i >= 0; i -= 1)
            {
                RenderSettings.ambientIntensity = (float)i / 100;
                Light.intensity = (float)i / 100;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
