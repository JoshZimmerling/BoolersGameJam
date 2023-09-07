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
    [SerializeField] private TMPro.TMP_InputField ipAddressInput;

    [SerializeField] private GameObject map;
    [SerializeField] private GameObject minimap;

    [SerializeField] private Button kaidenButton;
    [SerializeField] private Button drewButton;
    [SerializeField] private Button testButton;

    private void Awake()
    {
        hostButton.onClick.AddListener(() => {
            this.gameObject.SetActive(false);
            map.gameObject.SetActive(true);
            NetworkManager.Singleton.StartHost();
        });

        joinButton.onClick.AddListener(() => {
            // TODO: Check for actual IP
            if (ipAddressInput.text != "")
            {
                NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().ConnectionData.Address = ipAddressInput.text;
                this.gameObject.SetActive(false);
                map.gameObject.SetActive(true);
                NetworkManager.Singleton.StartClient();
            }
        });

        kaidenButton.onClick.AddListener(() => {
            NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().ConnectionData.Address = "131.93.247.207";
            this.gameObject.SetActive(false);
            map.gameObject.SetActive(true);
            NetworkManager.Singleton.StartClient();
        });

        drewButton.onClick.AddListener(() => {
            NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().ConnectionData.Address = "184.98.3.110";
            this.gameObject.SetActive(false);
            map.gameObject.SetActive(true);
            NetworkManager.Singleton.StartClient();
        });

        testButton.onClick.AddListener(() => {
            NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().ConnectionData.Address = "127.0.0.1";
            this.gameObject.SetActive(false);
            map.gameObject.SetActive(true);
            NetworkManager.Singleton.StartClient();
        });
    }
}

