using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    [SerializeField] private Light Light;
    [SerializeField] private LightingSettings settings;
    private static readonly int Intensity = Shader.PropertyToID("_Intensity_Multyplier");

    private float brightness = 0.84f;
    private bool change = false;

    // Start is called before the first frame update
    void Start()
    {
        //settings.
        //Debug.Log("ASDSA");
        //Light.intensity = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (change && Light.enabled)
        {
            Light.enabled = false;
        }
        if (change && !Light.enabled)
        {
            Light.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (tag == "Lighting Change")
        {
            change = false;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (tag == "Lighting Change")
        {
            change = true;
        }


        /*Debug.Log("ASDSAtgbrewu");
        if (tag == "Lighting Change")
        {
            Debug.Log("Asjerwiohj");
            if (brightness == 0.84f)
            {
                Debug.Log("1");
                while (brightness > 0)
                {
                    brightness -= 0.0001f;
                    Debug.Log(brightness);
                    skyBox.SetFloat(Exposure, brightness);
                    Update();
                }
            }
            if (brightness == 0f)
            {
                Debug.Log("2");
                while (brightness < 0.84f)
                {
                    brightness += 0.0001f;
                    Debug.Log(brightness);
                    skyBox.SetFloat(Exposure, brightness);
                    Update();
                }
            }
        }*/
    }
}
