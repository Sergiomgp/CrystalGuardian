using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;

    Animator enemyAnimations;

    Transform target;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyAnimations = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            ChaseAnimation();

            if (distance <= agent.stoppingDistance + .5)
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
    }

    private void IdleAnimation()
    {
        //idle animation
        enemyAnimations.SetBool("isNearPlayer", false);
        enemyAnimations.SetBool("isChasing", false);
        enemyAnimations.SetBool("isIdle", true);
    }


    private void ChaseAnimation()
    {
        //chases the target
        enemyAnimations.SetBool("isIdle", false);
        enemyAnimations.SetBool("isNearPlayer", false);
        enemyAnimations.SetBool("isChasing", true);
    }

    private void AttackAnimation()
    {
        //attack the target
        enemyAnimations.SetBool("isIdle", false);
        enemyAnimations.SetBool("isChasing", false);
        enemyAnimations.SetBool("isNearPlayer", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
