using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStats : MonoBehaviour
{
    public Slider enemyHealthBar;
    public GameObject heathBarUI;

    public float deathDelay;

    public bool isAlive;

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
        enemyAnimations = GetComponentInChildren<Animator>();
        currenthealth = maxhealth;
        enemyHealthBar.value = CalculateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealthBar.value = CalculateHealth();

        if (currenthealth < maxhealth)
        {
            heathBarUI.SetActive(true);
        }

        if (isAlive && currenthealth <= 0)
        {
            isAlive = false;
            enemyAnimations.SetTrigger("flyDeathStart");
            DeathAnimation();
        }

        if (currenthealth > maxhealth)
        {
            currenthealth = maxhealth;
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
        }

    }

    public void DeathAnimation()
    {
        enemyAnimations.SetTrigger("flyDeathEnd");
    }
}
