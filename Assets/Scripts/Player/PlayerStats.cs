using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    PlayerSystems pSystems;
    public bool isPlayerAlive;
    public float playerHp = 100f;
    public float currentHp;

    public float playerMana = 100f;
    public float currentMana;
    public bool hasMana;


    public bool isShielded;
    // Start is called before the first frame update

    void Awake()
    {
        isPlayerAlive = true;
        hasMana = true;
        isShielded = false;
        currentHp = playerHp;
        currentMana = playerMana;

        pSystems = GameObject.FindGameObjectWithTag("GameSystems").GetComponent<PlayerSystems>();
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //checks for shield
        if (Input.GetKeyDown(KeyCode.Q))
        {
             pSystems.ShieldCheck();
        }
        //debug stuff
        if (Input.GetKeyDown(KeyCode.F))
        {
            pSystems.PlayerTakeDamage(20);
        }
    }
}
