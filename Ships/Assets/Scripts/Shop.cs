using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject p1_destroyer;
    [SerializeField] GameObject p1_hawk;
    [SerializeField] GameObject p1_challenger;
    [SerializeField] GameObject p1_goliath;
    [SerializeField] GameObject p1_lightning;
    [SerializeField] GameObject p1_drone;
    [SerializeField] GameObject p1_scout;

    [SerializeField] GameObject p2_destroyer;
    [SerializeField] GameObject p2_hawk;
    [SerializeField] GameObject p2_challenger;
    [SerializeField] GameObject p2_goliath;
    [SerializeField] GameObject p2_lightning;
    [SerializeField] GameObject p2_drone;
    [SerializeField] GameObject p2_scout;

    [SerializeField] GameObject p1_destroyer_prefab;
    [SerializeField] GameObject p1_hawk_prefab;
    [SerializeField] GameObject p1_challenger_prefab;
    [SerializeField] GameObject p1_goliath_prefab;
    [SerializeField] GameObject p1_lightning_prefab;
    [SerializeField] GameObject p1_drone_prefab;
    [SerializeField] GameObject p1_scout_prefab;

    [SerializeField] GameObject p2_destroyer_prefab;
    [SerializeField] GameObject p2_hawk_prefab;
    [SerializeField] GameObject p2_challenger_prefab;
    [SerializeField] GameObject p2_goliath_prefab;
    [SerializeField] GameObject p2_lightning_prefab;
    [SerializeField] GameObject p2_drone_prefab;
    [SerializeField] GameObject p2_scout_prefab;

    // Start is called before the first frame update
    void Start()
    {
        Button p1_destroyer_button = p1_destroyer.GetComponent<Button>();
        p1_destroyer_button.onClick.AddListener(() => createShip(p1_destroyer_prefab, 20, 1));
        Button p1_hawk_button = p1_hawk.GetComponent<Button>();
        p1_hawk_button.onClick.AddListener(() => createShip(p1_hawk_prefab, 20, 1));
        Button p1_challenger_button = p1_challenger.GetComponent<Button>();
        p1_challenger_button.onClick.AddListener(() => createShip(p1_challenger_prefab, 20, 1));
        Button p1_goliath_button = p1_goliath.GetComponent<Button>();
        p1_goliath_button.onClick.AddListener(() => createShip(p1_goliath_prefab, 35, 1));
        Button p1_lightning_button = p1_lightning.GetComponent<Button>();
        p1_lightning_button.onClick.AddListener(() => createShip(p1_lightning_prefab, 35, 1));
        Button p1_drone_button = p1_drone.GetComponent<Button>();
        p1_drone_button.onClick.AddListener(() => createShip(p1_drone_prefab, 5, 1));
        Button p1_scout_button = p1_scout.GetComponent<Button>();
        p1_scout_button.onClick.AddListener(() => createShip(p1_scout_prefab, 10, 1));

        Button p2_destroyer_button = p2_destroyer.GetComponent<Button>();
        p2_destroyer_button.onClick.AddListener(() => createShip(p2_destroyer_prefab, 20, 2));
        Button p2_hawk_button = p2_hawk.GetComponent<Button>();
        p2_hawk_button.onClick.AddListener(() => createShip(p2_hawk_prefab, 20, 2));
        Button p2_challenger_button = p2_challenger.GetComponent<Button>();
        p2_challenger_button.onClick.AddListener(() => createShip(p2_challenger_prefab, 20, 2));
        Button p2_goliath_button = p2_goliath.GetComponent<Button>();
        p2_goliath_button.onClick.AddListener(() => createShip(p2_goliath_prefab, 35, 2));
        Button p2_lightning_button = p2_lightning.GetComponent<Button>();
        p2_lightning_button.onClick.AddListener(() => createShip(p2_lightning_prefab, 35, 2));
        Button p2_drone_button = p2_drone.GetComponent<Button>();
        p2_drone_button.onClick.AddListener(() => createShip(p2_drone_prefab, 5, 2));
        Button p2_scout_button = p2_scout.GetComponent<Button>();
        p2_scout_button.onClick.AddListener(() => createShip(p2_scout_prefab, 10, 2));
    }

    private void createShip(GameObject shipType, float cost, int playerNum)
    {
        //Add a check for gold based on player number
        GameObject ship = Instantiate(shipType);
        ship.GetComponent<NetworkObject>().Spawn();
    }
}
