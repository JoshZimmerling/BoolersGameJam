using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button serverButton;
    [SerializeField] private GameObject ipAddress;
    [SerializeField] private GameObject map;

    private void Awake()
    {
        hostButton.onClick.AddListener(() => {
            this.gameObject.SetActive(false);
            map.gameObject.SetActive(true);
            NetworkManager.Singleton.StartHost();
        });

        joinButton.onClick.AddListener(() => {
            // TODO: Check for actual IP
            this.gameObject.SetActive(false);
            map.gameObject.SetActive(true); 
            if (ipAddress.GetComponent<Text>().text == "")
                NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().ConnectionData.Address = ipAddress.GetComponent<Text>().text;
            else
                NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().ConnectionData.Address = "131.93.247.207";
            NetworkManager.Singleton.StartClient();
        });
    }
}

