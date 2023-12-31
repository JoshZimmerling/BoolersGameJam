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
    [SerializeField] private TMPro.TMP_InputField ipAddress;
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
            Debug.Log(ipAddress.text);
            if (ipAddress.text != "")
                NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().ConnectionData.Address = ipAddress.text;
            else
                NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().ConnectionData.Address = "131.93.247.207";

            this.gameObject.SetActive(false);
            map.gameObject.SetActive(true);
            
            NetworkManager.Singleton.StartClient();
        });
    }
}

