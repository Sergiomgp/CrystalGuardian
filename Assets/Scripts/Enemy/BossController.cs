using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    [SerializeField] GameObject ChargeBall;
    [SerializeField] GameObject Projectile;
    [SerializeField] GameObject Shield;
    public float lookRadius = 10f;

    BossStats boss;
    public Animator enemyAnimations;

    Transform target;
    NavMeshAgent agent;

    public bool attacking;
    public bool AirPhase;
    public bool BossEngaged;
    public bool AirPhaseComplete;
    public bool shieldActive;

    float attackCooldown = 3f;

    Transform playerPosition;


    // Start is called before the first frame update
    void Start()
    {
        shieldActive = false;
        AirPhaseComplete = false;
        BossEngaged = false;
        AirPhase = false;
        attacking = false;
        boss = GetComponent<BossStats>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyAnimations = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.isAlive)
        {
            playerPosition = target.transform;
            float distance = Vector3.Distance(target.position, transform.position);
            //checks for the distance between enemy and player when its less the the look radius starts chasing player
            if (distance <= lookRadius)
            {
                BossEngaged = true;
                TakeOffAnimation();
                FaceTarget();
                if (BossEngaged && AirPhase && !AirPhaseComplete && !attacking && boss.isAlive)
                {
                    StartCoroutine(GenerateAttack());
                }
            }

            if (distance > lookRadius)
            {
                IdleAnimation();
            }
        }
        else if (!boss.isAlive)
        {
            Dead();
        }
    }

    private void Dead()
    {
        BossEngaged = true;
        AirPhase = true;
        AirPhaseComplete = true;
        agent.enabled = true;

        Destroy(gameObject, 2f);
    }

    #region AirPhase Methods
    private void IdleAnimation()
    {
        enemyAnimations.SetFloat("locomotion", 0.5f);
    }

    private void TakeOffAnimation()
    {
        if (BossEngaged && !AirPhase && !AirPhaseComplete)
        {
            agent.isStopped = true;
            agent.enabled = false;
            AirPhase = true;
            enemyAnimations.SetTrigger("idleTakeoff");

        }
        else { return; }
    }

    private void AirBlast()
    {
        if (!attacking && boss.isAlive)
        {
            attacking = true;
            enemyAnimations.SetTrigger("flyAttack");
            ChargeBall.SetActive(true);
            StartCoroutine(ProjectileAttackCooldown());
        }
    }

    private IEnumerator ProjectileAttackCooldown()
    {
        Invoke("InstantiateAttack", 1.2f);
        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }

    private void AirGlide()
    {
        if (boss.isAlive)
        {
            attacking = true;
            enemyAnimations.SetTrigger("flyGlide");
            StartCoroutine(AttackCooldown());
        }
        else { return; }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }

    void InstantiateAttack()
    {
        ChargeBall.SetActive(false);
        var projectilObj = Instantiate(Projectile, ChargeBall.transform.position, Quaternion.identity) as GameObject;

    }
  
    private void GenerateShield()
    {
        if (!shieldActive && boss.isAlive)
        {
            shieldActive = true;
            Shield.SetActive(true);
            StartCoroutine(ShieldDuration());
        }
        else { return; }
    }

    private IEnumerator GenerateAttack()
    {
        if (boss.isAlive)
        {
            yield return new WaitForSeconds(5f);
            if (!attacking)
            {
                float randValue = Random.value;
                if (randValue < .45f) // 45% of the time
                {
                    AirBlast();
                }
                else if (randValue < .9f) // 45% of the time
                {
                    AirGlide();
                }
                else // 10% of the time
                {
                    GenerateShield();
                    // Do Special Attack
                }
            }
        }
    }
    #endregion


    private IEnumerator ShieldDuration()
    {
        yield return new WaitForSeconds(7f);
        Shield.SetActive(false);
        shieldActive = false;
    }



    #region FaceTarget
    //rotates enemy to face the player
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1f);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    #endregion
}
