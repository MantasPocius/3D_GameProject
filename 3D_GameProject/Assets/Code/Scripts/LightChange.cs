using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    [SerializeField] private Material skyBox;
    private static readonly int Exposure = Shader.PropertyToID("_Exposure");
    private float brightness = 0.84f;
    private bool change = false;

    // Start is called before the first frame update
    void Start()
    {
        skyBox.SetFloat(Exposure, brightness);
        Debug.Log("ASDSA");
    }

    // Update is called once per frame
    void Update()
    {
        if (change)
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
                }
            }
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
