using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AutoHost : MonoBehaviour
{
    private void Start()
    {
        NetworkManager.Singleton.StartHost(); 
    }
}
