using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    //projectile collision script for destroying upon impact with other objects
    PlayerAttack player;
    //reference variables//
    //player script
    private float spellChargeTime;
    private float currentChargeTime;
    //enemy script
    private string enemyWeakness;
    private string enemyResistence;


    //damage variables
    [SerializeField] private float baseDamage;
    private float damageMultiplier = 2f;
    private float finalDamage;
    [SerializeField] private string spellType;

    private bool collided;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        CalculateDamage();
    }

    //calculates the base damage of the spell based on the charged time
    void CalculateDamage()
    {
        spellChargeTime = player.newChargeTime;

        //Debug.Log("charge time is " + spellChargeTime);

        if (spellChargeTime <= 1)
        {
            baseDamage = 15;
        }

        if (spellChargeTime > 1 && spellChargeTime < 2)
        {
            baseDamage = 25;
        }

        if (spellChargeTime >= 2)
        {
            baseDamage = 50;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //checks for collision with other objets that arent enemies
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Enemy" && !collided)
        {
            collided = true;
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("collider with" + collision.gameObject.name);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }

        //checks for the tag of the collided enemy and damages it according to the enemy weakness and resistence
        if (collision.gameObject.tag == "Enemy")
        {
            enemyWeakness = collision.gameObject.GetComponent<EnemyStats>().weakness;
            enemyResistence = collision.gameObject.GetComponent<EnemyStats>().resistence;

            //doubles the damage
            if (enemyWeakness == spellType)
            {
                finalDamage = baseDamage * damageMultiplier;
                collision.gameObject.GetComponent<EnemyStats>().TakeDamage(finalDamage);
                Destroy(gameObject);
            }
            //halves the damage
            if (enemyResistence == spellType)
            {
                finalDamage = baseDamage / damageMultiplier;
                collision.gameObject.GetComponent<EnemyStats>().TakeDamage(finalDamage);
                Destroy(gameObject);

            }
            //does base damage
            if (enemyResistence != spellType && enemyWeakness != spellType)
            {
                collision.gameObject.GetComponent<EnemyStats>().TakeDamage(baseDamage);
                Destroy(gameObject);
            }
        }
    }
}
