using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip GotHit, Attack, Dead;

    public float lookRadius = 10f;

    EnemyStats enemy;
    public Animator enemyAnimations;

    Transform target;
    NavMeshAgent agent;

    public bool attacking;
    public bool enemyEngaged;

    float distanceOffset = .7f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        enemyEngaged = false;
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
            enemyEngaged = true;
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
            enemyEngaged = false;
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
        attacking = false;
        enemyAnimations.SetFloat("locomotion", 0);
        enemyAnimations.SetBool("attack1", false);
    }

    private void ChaseAnimation()
    {
        attacking = false;
        agent.isStopped = false;
        enemyAnimations.SetFloat("locomotion", 1f);
        enemyAnimations.SetBool("attack1", false);
    }

    private void AttackAnimation()
    {
        agent.isStopped = true;
        enemyAnimations.SetFloat("locomotion", 0f);
        enemyAnimations.SetBool("attack1", true);
        attacking = true;
    }

    private void DeathAnimation()
    {
        if (enemy.currenthealth <= 0)
        {
            enemyAnimations.SetBool("death", true);
            agent.isStopped = true;
            agent.enabled = false;
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

    void PlayATKSfx()
    {
        audioSource.PlayOneShot(Attack, 0.6f);
    }

    void PlayDeathSfx()
    {
        audioSource.PlayOneShot(Dead, 0.6f);
    }

    void PlayGotHitSfx()
    {
        audioSource.PlayOneShot(GotHit, 0.6f);
    }

}
