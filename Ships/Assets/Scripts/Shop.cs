using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject destroyer;
    [SerializeField] GameObject hawk;
    [SerializeField] GameObject challenger;
    [SerializeField] GameObject goliath;
    [SerializeField] GameObject lightning;
    [SerializeField] GameObject drone;
    [SerializeField] GameObject scout;

    Button destroyer_button;
    Button hawk_button;
    Button challenger_button;
    Button goliath_button;
    Button lightning_button;
    Button drone_button;
    Button scout_button;

    [SerializeField] Sprite p1_destroyer;
    [SerializeField] Sprite p1_hawk;
    [SerializeField] Sprite p1_challenger;
    [SerializeField] Sprite p1_goliath;
    [SerializeField] Sprite p1_lightning;
    [SerializeField] Sprite p1_drone;
    [SerializeField] Sprite p1_scout;

    [SerializeField] Sprite p2_destroyer;
    [SerializeField] Sprite p2_hawk;
    [SerializeField] Sprite p2_challenger;
    [SerializeField] Sprite p2_goliath;
    [SerializeField] Sprite p2_lightning;
    [SerializeField] Sprite p2_drone;
    [SerializeField] Sprite p2_scout;

    [SerializeField] public GameObject p1_destroyer_prefab;
    [SerializeField] public GameObject p1_hawk_prefab;
    [SerializeField] public GameObject p1_challenger_prefab;
    [SerializeField] public GameObject p1_goliath_prefab;
    [SerializeField] public GameObject p1_lightning_prefab;
    [SerializeField] public GameObject p1_drone_prefab;
    [SerializeField] public GameObject p1_scout_prefab;

    [SerializeField] public GameObject p2_destroyer_prefab;
    [SerializeField] public GameObject p2_hawk_prefab;
    [SerializeField] public GameObject p2_challenger_prefab;
    [SerializeField] public GameObject p2_goliath_prefab;
    [SerializeField] public GameObject p2_lightning_prefab;
    [SerializeField] public GameObject p2_drone_prefab;
    [SerializeField] public GameObject p2_scout_prefab;

    bool shopOpenedBefore = false;
    GameObject parent;
    int playerNum;
    PlayerController parentPlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        destroyer_button = destroyer.GetComponent<Button>();
        hawk_button = hawk.GetComponent<Button>();
        challenger_button = challenger.GetComponent<Button>();
        goliath_button = goliath.GetComponent<Button>();
        lightning_button = lightning.GetComponent<Button>();
        drone_button = drone.GetComponent<Button>();
        scout_button = scout.GetComponent<Button>();
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }

    public void openShop(GameObject player)
    {
        parent = player;
        playerNum = (int)player.GetComponent<NetworkObject>().OwnerClientId + 1;
        parentPlayerScript = player.GetComponent<PlayerController>();

        if (this.gameObject.activeSelf == false)
        {
            this.gameObject.SetActive(true);
            if (shopOpenedBefore == false)
            {
                if (playerNum == 1)
                {
                    destroyer_button.image.sprite = p1_destroyer;
                    hawk_button.image.sprite = p1_hawk;
                    challenger_button.image.sprite = p1_challenger;
                    goliath_button.image.sprite = p1_goliath;
                    lightning_button.image.sprite = p1_lightning;
                    drone_button.image.sprite = p1_drone;
                    scout_button.image.sprite = p1_scout;
                }
                else
                {
                    destroyer_button.image.sprite = p2_destroyer;
                    hawk_button.image.sprite = p2_hawk;
                    challenger_button.image.sprite = p2_challenger;
                    goliath_button.image.sprite = p2_goliath;
                    lightning_button.image.sprite = p2_lightning;
                    drone_button.image.sprite = p2_drone;
                    scout_button.image.sprite = p2_scout;
                }

                destroyer_button.onClick.AddListener(() => parentPlayerScript.createShipServerRPC(Ship.shipTypes.Destroyer, 20, playerNum));
                hawk_button.onClick.AddListener(() => parentPlayerScript.createShipServerRPC(Ship.shipTypes.Hawk, 20, playerNum));
                challenger_button.onClick.AddListener(() => parentPlayerScript.createShipServerRPC(Ship.shipTypes.Challenger, 20, playerNum));
                goliath_button.onClick.AddListener(() => parentPlayerScript.createShipServerRPC(Ship.shipTypes.Goliath, 35, playerNum));
                lightning_button.onClick.AddListener(() => parentPlayerScript.createShipServerRPC(Ship.shipTypes.Lightning, 35, playerNum));
                drone_button.onClick.AddListener(() => parentPlayerScript.createShipServerRPC(Ship.shipTypes.Drone, 5, playerNum));
                scout_button.onClick.AddListener(() => parentPlayerScript.createShipServerRPC(Ship.shipTypes.Scout, 10, playerNum));
            }
            shopOpenedBefore = true;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    
}
