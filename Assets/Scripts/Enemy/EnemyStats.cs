using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{

    public Slider enemyHealthBar;
    public GameObject heathBarUI;
    private GameObject death_particles;

    public string weakness;
    public string resistence;
    public float maxhealth;
    public float currenthealth;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        //death_particles = GameObject.FindGameObjectWithTag("DeathParticles");
        //death_particles.SetActive(false);
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
            //dead animation
            //dead soundFX
            Destroy(gameObject, .5f);
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
    }
}
