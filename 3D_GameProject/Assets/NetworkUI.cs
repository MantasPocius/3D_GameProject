using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button ClientButton;
    // Start is called before the first frame update
    private void Awake()
    {
       hostButton.onClick.AddListener(() =>
       {
              NetworkManager.Singleton.StartHost();
       });
         ClientButton.onClick.AddListener(() =>
         {
                  NetworkManager.Singleton.StartClient();
         });
    }
    
         
    }

    

