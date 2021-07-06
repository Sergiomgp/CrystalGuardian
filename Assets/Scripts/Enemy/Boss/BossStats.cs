using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStats : MonoBehaviour
{
    public GameObject DeathCutscene;
    public Slider enemyHealthBar;
    public GameObject heathBarUI;
    public GameObject bar;
    public GameObject playerHud;

    public float deathDelay;

    public bool isAlive;
    public static bool bossAlive;
    Animator enemyAnimations;

    BossController boss;

    public string weakness;
    public string resistence;
    public float maxhealth;
    public float currenthealth;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<BossController>();
        isAlive = true;
        bossAlive = isAlive;
        enemyAnimations = GetComponentInChildren<Animator>();
        currenthealth = maxhealth;
        enemyHealthBar.value = CalculateHealth();
    }

    // Update is called once per frame
    void Update()
    {

        enemyHealthBar.value = CalculateHealth();
        EnableUI();

        if (currenthealth < maxhealth)
        {
            heathBarUI.SetActive(true);
        }

        if (isAlive && currenthealth <= 0)
        {
            isAlive = false;
            bossAlive = isAlive;
        }

        if (currenthealth > maxhealth)
        {
            currenthealth = maxhealth;
        }

        if (!isAlive)
        {
            StartCoroutine(StartCutscene());
        }
        
    }

    float CalculateHealth()
    {
        return currenthealth / maxhealth;
    }

    public void TakeDamage(float ammount)
    {
        if (isAlive && boss.BossEngaged && boss.damageble)
        {
            currenthealth -= ammount;
            enemyAnimations.SetTrigger("flyGotHit");
            boss.PlayGotHitSfx();
        }

    }

    public void DeathAnimation()
    {
        enemyAnimations.SetTrigger("flyDeathEnd");
    }

    void EnableUI()
    {
        if (boss.BossEngaged)
        {
            bar.SetActive(true);
        }
    }

    void StartDeathCutscene()
    {
        if (!isAlive)
        {
            bar.SetActive(false);
            playerHud.SetActive(false);
            DeathCutscene.SetActive(true);
        }
    }

    IEnumerator StartCutscene()
    {
        yield return new WaitForSeconds(.1f);
        StartDeathCutscene();
    }
}
