using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSystems : MonoBehaviour
{
    PlayerStats player;

    public GameObject lHand;
    public GameObject rHand;


    #region MANA SYSTEM VARIABLES
    //UI STUFF
    public GameObject shieldEnabled, shieldDisabled;

    public Slider manaBar;
    public Text manaText;
    // SYSTEM RELATED VARIABLES
    private float manaRegenDelay = 2f;
    private WaitForSeconds regenTick = new WaitForSeconds(1.5f);
    private Coroutine regen;
    #endregion

    #region SHIELD SYSTEM VARIABLES
    [SerializeField] private bool onCooldown;
    private GameObject shield;
    private float cooldownTime = 5f;
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
        if (player.currentMana > 6f)
        {
            player.hasMana = true;
        }

        if (player.currentMana < 5f)
        {
            player.hasMana = false;
        }

        if (player.currentMana > player.playerMana)
        {
            player.currentMana = player.playerMana;
            updateManaUI();
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

        while (player.currentMana < player.playerMana)
        {
            player.currentMana += player.playerMana / 75;
            updateManaUI();
            yield return regenTick;
        }
        regen = null;
    }


    public void RestoreMana()
    {
        player.currentMana = player.playerMana;
        updateManaUI();

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
        shieldDisabled.SetActive(false);
        shieldEnabled.SetActive(true);
    }

    private IEnumerator ActivateShield()
    {
        shieldEnabled.SetActive(false);
        shieldDisabled.SetActive(true);
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
            UpdateHealthBarUI();

        }
        //checks if player used shield, if true, player takes no damage

        CheckHP();
    }

    void CheckHP()
    {
        if (player.currentHp <= 0 && PauseMenu.isGameOver == false)
        {
            player.isPlayerAlive = false;
            lHand.SetActive(false);
            rHand.SetActive(false);
            PauseMenu.isGameOver = true;
            //end game
        }
    }

    public void RestoreHP()
    {
        Debug.Log("Player restored " + (player.playerHp - player.currentHp));
        player.currentHp = player.playerHp;
        UpdateHealthBarUI();

    }

    void UpdateHealthBarUI()
    {
        hpBar.value = player.currentHp;
        hp_text.text = player.currentHp.ToString("F0");
    }


    #endregion
}
