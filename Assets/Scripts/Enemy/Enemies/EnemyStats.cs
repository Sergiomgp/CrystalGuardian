using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public Slider enemyHealthBar;
    public GameObject healthBarUI;
    private GameObject death_particles;

    EnemyController controller;

    public float deathDelay;

    bool isAlive;
    public int died;

    Animator enemyAnimations;

    public string weakness;
    public string resistence;
    public float maxhealth;
    public float currenthealth;
    public float damage;
    public string zone;

    public GameObject prision;
    public bool imprisioned;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        enemyAnimations = GetComponentInChildren<Animator>();
        controller = GetComponent<EnemyController>();
        currenthealth = maxhealth;
        enemyHealthBar.value = CalculateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealthBar.value = CalculateHealth();

        if (currenthealth < maxhealth)
        {
            healthBarUI.SetActive(true);
        }

        if (isAlive && currenthealth <= 0)
        {
            isAlive = false;
            gameObject.GetComponent<Enemy>().Kill();
        }

        if (currenthealth > maxhealth || controller.enemyEngaged == false)
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
        if (controller.enemyEngaged == true)
        {
            healthBarUI.SetActive(true);
            currenthealth -= ammount;
            enemyAnimations.SetBool("gotHit", true);
            enemyAnimations.SetFloat("locomotion", -1f);
        }

    }

    public void SecondaryEffect(bool dot)
    {
        if (dot && controller.enemyEngaged == true)
        {
            StartCoroutine(Imprision());
        }
    }

    private IEnumerator Imprision()
    {
        imprisioned = true;
        prision.SetActive(true);
        yield return new WaitForSeconds(3f);
        prision.SetActive(false);
        imprisioned = false;
    }

}
