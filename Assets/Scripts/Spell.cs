using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    Rigidbody rb;
    //projectile collision script for destroying upon impact with other objects
    PlayerAttack player;

    RaycastHit hit;
    //Collision Particle Effects
    public GameObject collisionExplosion;
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

        //ignores collisions between the spells and the player
        rb = gameObject.GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(0, 9);
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
        //instantiates explosion particles in the position of the collision
        if (Physics.Raycast(rb.transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            Instantiate(collisionExplosion, transform.position, Quaternion.identity);
        }
        //checks for collision with other objets that arent enemies
        if (collision.gameObject.tag != "Spell" && collision.gameObject.tag != "Enemy" && !collided)
        {
            collided = true;
            Destroy(gameObject);
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
            }
            //halves the damage
            if (enemyResistence == spellType)
            {
                finalDamage = baseDamage / damageMultiplier;
                collision.gameObject.GetComponent<EnemyStats>().TakeDamage(finalDamage);
            }
            //does base damage
            if (enemyResistence != spellType && enemyWeakness != spellType)
            {
                collision.gameObject.GetComponent<EnemyStats>().TakeDamage(baseDamage);
            }
            Destroy(gameObject);
        }
    }
}
