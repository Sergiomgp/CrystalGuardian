using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    PlayerSystems player;
    EnemyStats enemy;
    bool isDamaged;

    float damage;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("GameSystems").GetComponent<PlayerSystems>();
        enemy = GetComponentInParent<EnemyStats>();
    }

    private void Start()
    {
        damage = enemy.damage;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && !isDamaged)
        {
            Debug.Log("hitted" + other.gameObject.name);
            player.PlayerTakeDamage(damage);
            isDamaged = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isDamaged = false;
    }
}
