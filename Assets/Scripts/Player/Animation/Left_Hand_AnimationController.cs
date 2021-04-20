using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left_Hand_AnimationController : MonoBehaviour
{
    PlayerAttack chargeAttack;
    private Animator anim;
    private GameObject petraEffect;
    private PlayerAttack LH_shoot;

    ParticleSystem charge;

    // Start is called before the first frame update
    void Start()
    {
        chargeAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        anim = GetComponent<Animator>();
        LH_shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        petraEffect = GameObject.FindGameObjectWithTag("PetraParticle");
        charge = petraEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //triggers animation on the left hand and enables charge effect gameobject
        if (LH_shoot.leftHand)
        {
            anim.SetBool("IsPressed", true);
            petraEffect.SetActive(true);
            ChargeEmissionRate();
            //Debug.Log("LH_SHOOT is now " + LH_shoot.leftHand);
        }

        if (!LH_shoot.leftHand)
        {
            anim.SetBool("IsPressed", false);
            petraEffect.SetActive(false);
            //Debug.Log("LH_SHOOT is now " + LH_shoot.leftHand);
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
            emission.rateOverTime = 15;
        }
    }
}
