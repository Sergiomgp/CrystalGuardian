using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAttackCollision : MonoBehaviour
{

    PlayerSystems _pSystems;
    EnemyController _enemy;
    EnemyStats _enemyStats;
    BoxCollider handCollider;

    private void Awake()
    {
        _pSystems = GameObject.FindGameObjectWithTag("GameSystems").GetComponent<PlayerSystems>();
        _enemy = GetComponentInParent<EnemyController>();
        _enemyStats = GetComponentInParent<EnemyStats>();
        handCollider = GetComponent<BoxCollider>();
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            Physics.IgnoreCollision(handCollider, other.GetComponent<Collider>());
        }

        if (other.gameObject.tag == "Player" && _enemy.attacking)
        {
            _pSystems.PlayerTakeDamage(_enemyStats.damage);
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        handCollider.enabled = false;
        yield return new WaitForSeconds(1.2f);
        handCollider.enabled = true;
    }
}
