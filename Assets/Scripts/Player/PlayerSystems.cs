using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSystems : MonoBehaviour
{
    PlayerStats player;


    #region MANA SYSTEM VARIABLES
    //UI STUFF
    public Slider manaBar;
    public Text manaText;
    // SYSTEM RELATED VARIABLES
    private float manaRegenDelay = 2f;
    private WaitForSeconds regenTick = new WaitForSeconds(1f);
    private Coroutine regen;
    #endregion

    #region SHIELD SYSTEM VARIABLES
    [SerializeField]private bool onCooldown;
    private GameObject shield;
    private float cooldownTime = 2f;
    private Coroutine ShieldCD;

    #endregion

    #region HP SYSTEM VARIABLES
    public Slider hpBar;
    public Text hp_text;


    #endregion



    // Start is called before the first frame update
    void Awake()
    {
        //getting reference for player stats script
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        //getting reference to the shield object
        shield = GameObject.FindGameObjectWithTag("Shield");
    }

    void Start()
    {

        manaBar.maxValue = player.currentMana;
        manaText.text = player.currentMana.ToString();

        hpBar.maxValue = player.playerHp;
        hpBar.value = player.currentHp;
        hp_text.text = player.currentHp.ToString();

        shield.SetActive(false);
    }

    private void Update()
    {
        ManaCheck();
    }

    #region MANA SYSTEM METHODS
    void ManaCheck()
    {
        if (player.currentMana > 5)
        {
            player.hasMana = true;
        }

        if (player.currentMana <= 4)
        {
            player.hasMana = false;
        }

        if (player.currentMana > player.playerMana)
        {
            player.currentMana = player.playerMana;
        }
    }

    
    public void ConsumeMana(int ammount)
    {
        ManaCheck();
        if (player.hasMana && player.currentMana - ammount > 0)
        {
            player.currentMana -= ammount;
            updateManaUI();

            if (regen != null)
            {
                StopCoroutine(regen);
            }
            regen = StartCoroutine(RegenMana());
        }
        else if (player.hasMana && player.currentMana - ammount < 0)
        {
            player.hasMana = false;
            player.currentMana = 0;
        }
    }

    void updateManaUI()
    {
        manaBar.value = player.currentMana;
        manaText.text = player.currentMana.ToString("F0");
    }


    //Regens the player´s mana 
    private IEnumerator RegenMana()
    {
        yield return new WaitForSeconds(manaRegenDelay);

        while(player.currentMana < player.playerMana)
        {
            player.currentMana += player.playerMana / 50;
            updateManaUI();
            yield return regenTick;
        }
        regen = null;
    }
    #endregion

    #region SHIELD SYSTEM METHODS

    public void ShieldCheck()
    {
        if (!player.isShielded && !onCooldown)
        {
            StartCoroutine(ActivateShield());
        }
    }

    private IEnumerator ShieldColdown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
    }

    private IEnumerator ActivateShield()
    {
        //changes player tag to shielded so it doesnt take damage during its duration
        player.isShielded = true;
        shield.SetActive(true);
        //play sound effects for the shield

        yield return new WaitForSeconds(3f);

        //reverts the tag back to player so damage is calculated normally
        player.isShielded = false;
        shield.SetActive(false);

        //starts shield cooldown
        StartCoroutine(ShieldColdown());
    }

    #endregion

    #region HP SYSTEM METHODS
    
    public void PlayerTakeDamage(float ammount)
    {
        
        if (!player.isShielded && player.isPlayerAlive)
        {
            player.currentHp -= ammount;
            hpBar.value = player.currentHp;
            hp_text.text = player.currentHp.ToString("F0");

        }
        //checks if player used shield, if true, player takes no damage
     
        CheckHP();
    }

    void CheckHP()
    {
        if (player.currentHp <= 0)
        {
            player.isPlayerAlive = false;
            //end game
        }
    }


    #endregion
}
