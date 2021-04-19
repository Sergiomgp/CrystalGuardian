using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_Hand_AnimationController : MonoBehaviour
{
    private Animator anim;
    private GameObject flameEffect;
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
        //triggers animation on the right hand and enables charge effect gameobject
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
