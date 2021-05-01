using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{

    public Slider enemyHealthBar;
    public GameObject heathBarUI;
    private GameObject death_particles;

    public float deathDelay;
    

    Animator enemyAnimations;

    public string weakness;
    public string resistence;
    public float maxhealth;
    public float currenthealth;
    public float damage;

    public GameObject prision;
    public bool imprisioned;

    // Start is called before the first frame update
    void Start()
    {
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

        if (currenthealth <= 0)
        {
            //death_particles.SetActive(true);
            //dead soundFX
            Destroy(gameObject, deathDelay);
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
        currenthealth -= ammount;
        enemyAnimations.SetBool("gotHit", true);
        enemyAnimations.SetFloat("locomotion", -1f);
    }

    public void SecondaryEffect(bool dot)
    {
        if (dot)
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
