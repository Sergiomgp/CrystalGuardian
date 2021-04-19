using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_Hand_AnimationController : MonoBehaviour
{
    private Animator anim;
    private GameObject flameEffect;

    // nao me lembro do que faz isto checkar depois
    //[SerializeField]private ParticleSystem rhChargeEffect;

    private PlayerAttack RH_shoot;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        RH_shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        flameEffect = GameObject.FindGameObjectWithTag("FlameParticle");
    }

    // Update is called once per frame
    void Update()
    {
        if (RH_shoot.rightHand)
        {
            anim.SetBool("IsPressed", true);
            flameEffect.SetActive(true);
        }

        if (!RH_shoot.rightHand)
        {
            anim.SetBool("IsPressed", false);
            flameEffect.SetActive(false);
        }
    }
}
