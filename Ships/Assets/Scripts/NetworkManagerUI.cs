using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button serverButton;
    //[SerializeField] private 

    private void Awake()
    {
        hostButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
            this.gameObject.SetActive(false);
        });

        joinButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
            this.gameObject.SetActive(false);
        });
    }
}
