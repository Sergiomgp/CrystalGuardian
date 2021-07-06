using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    [SerializeField] GameObject ChargeBall;
    [SerializeField] GameObject Projectile;
    [SerializeField] GameObject Shield;

    public AudioSource audioSource;
    public AudioClip roarSFX;
    public AudioClip gotHitSfx;
    public float lookRadius = 10f;

    BossStats boss;
    public Animator enemyAnimations;

    Transform target;
    NavMeshAgent agent;

    public bool attacking;
    public bool AirPhase;
    public bool BossEngaged;
    public static bool engaged;
    public bool AirPhaseComplete;
    public bool shieldActive;

    public bool damageble;
    float attackCooldown = 1.5f;

    Transform playerPosition;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        damageble = false;
        shieldActive = false;
        AirPhaseComplete = false;
        BossEngaged = false;
        engaged = BossEngaged;
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
                engaged = BossEngaged;
                StartCoroutine(Invincible());
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
        BossStats.bossAlive = false;
        Debug.Log(BossStats.bossAlive);
        BossEngaged = false;
        engaged = false;
        AirPhase = false;
        AirPhaseComplete = true;
        agent.enabled = true;

        Destroy(gameObject, 2f);
    }

    #region AirPhase Methods
    //runs idleanimation
    private void IdleAnimation()
    {
        enemyAnimations.SetFloat("locomotion", 0.5f);
    }
    //runs TakeOffAnimation and disables navmeshagent component
    private void TakeOffAnimation()
    {
        if (BossEngaged && !AirPhase && !AirPhaseComplete && boss.isAlive)
        {
            agent.isStopped = true;
            agent.enabled = false;
            AirPhase = true;
            enemyAnimations.SetTrigger("idleTakeoff");
            audioSource.PlayOneShot(roarSFX);

        }
        else { return; }
    }

    //makes the boss undamageble during takeOff Animation
    private IEnumerator Invincible()
    {
        yield return new WaitForSeconds(3f);
        damageble = true;
    }

    //does an airblast attack charge
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

    //instantiates chargeblast at the player position
    void InstantiateAttack()
    {
        ChargeBall.SetActive(false);
        var projectilObj = Instantiate(Projectile, ChargeBall.transform.position, Quaternion.identity) as GameObject;

    }

    //launches charge blast
    private IEnumerator ProjectileAttackCooldown()
    {
        Invoke("InstantiateAttack", 1.2f);
        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }

    //glides over the player
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

    //boss abilities cooldown
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }


  
    //activates boss shield preventing him from taking damage for a short time
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

    //deactivates the shield after a few seconds
    private IEnumerator ShieldDuration()
    {
        yield return new WaitForSeconds(7f);
        Shield.SetActive(false);
        shieldActive = false;
    }

    public void PlayGotHitSfx()
    {
        audioSource.PlayOneShot(gotHitSfx, .5f);
    }

    //randomizes what abilites the boss will use based on a random number
    private IEnumerator GenerateAttack()
    {
        if (boss.isAlive)
        {
            yield return new WaitForSeconds(5f);
            if (!attacking && boss.isAlive)
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
