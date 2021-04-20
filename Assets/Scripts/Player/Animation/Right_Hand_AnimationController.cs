using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_Hand_AnimationController : MonoBehaviour
{
    PlayerAttack chargeAttack;
    private Animator anim;
    private GameObject flameEffect;
    private GameObject fullyCharged;
    private PlayerAttack RH_shoot;

    ParticleSystem charge;

    // Start is called before the first frame update

    void Start()
    {
        chargeAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        anim = GetComponent<Animator>();
        RH_shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        flameEffect = GameObject.FindGameObjectWithTag("FlameParticle");
        charge = flameEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //triggers animation on the right hand and enables charge effect gameobject
        if (RH_shoot.rightHand)
        {
            anim.SetBool("IsPressed", true);
            flameEffect.SetActive(true);
            ChargeEmissionRate();

        }

        if (!RH_shoot.rightHand)
        {
            anim.SetBool("IsPressed", false);
            flameEffect.SetActive(false);
        }
    }
    //changes the emission rate of the particleSystem to give a better visual feedback when the attack is fully charged
    public void ChargeEmissionRate()
    {
        var emission = charge.emission;
        var disableEmisison = charge.emission;

        if (chargeAttack.chargeTime < 0.7)
        {
            emission.rateOverTime = 0;
        }
        else if (chargeAttack.chargeTime < 1)
        {
            emission.rateOverTime = 5;
        }
        else if (chargeAttack.chargeTime < 2)
        {
            emission.rateOverTime = 7;
        }
        else
        {
            emission.rateOverTime = 12;
        }
    }
}
