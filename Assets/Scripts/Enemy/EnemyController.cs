using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;

    EnemyStats enemy;
    Animator enemyAnimations;

    Transform target;
    NavMeshAgent agent;

    float distanceOffset = .7f;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyAnimations = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        //checks for the distance between enemy and player when its less the the look radius starts chasing player
        if (distance <= lookRadius)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            ChaseAnimation();

            //when it reaches stopping distance stops chasing faces player and attacks
            if (distance <= agent.stoppingDistance + distanceOffset)
            {
                FaceTarget();
                AttackAnimation();
            }
        }

        if (distance > lookRadius)
        {
            IdleAnimation();
            agent.isStopped = true;
        }

        if (enemy.imprisioned)
        {
            agent.isStopped = true;
            IdleAnimation();
        }

        DeathAnimation();
    }

    private void IdleAnimation()
    {
        //idle animation
        enemyAnimations.SetFloat("locomotion", 0);
        enemyAnimations.SetBool("attack1", false);
    }

    private void ChaseAnimation()
    {
        //chases the target
        agent.isStopped = false;
        enemyAnimations.SetFloat("locomotion", 1f);
        enemyAnimations.SetBool("attack1", false);
    }

    private void AttackAnimation()
    {
        //attack the target
        agent.isStopped = true;
        enemyAnimations.SetFloat("locomotion", 0f);
        enemyAnimations.SetBool("attack1", true);
    }

    private void DeathAnimation()
    {
        if (enemy.currenthealth <= 0)
        {
            enemyAnimations.SetBool("death", true);
            agent.isStopped = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    //rotates enemy to face the player
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
