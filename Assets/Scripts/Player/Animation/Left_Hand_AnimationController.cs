using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left_Hand_AnimationController : MonoBehaviour
{
    private Animator anim;
    private GameObject windEffect;



    private PlayerAttack LH_shoot;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        LH_shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        windEffect = GameObject.FindGameObjectWithTag("WindParticle");
    }

    // Update is called once per frame
    void Update()
    {
        if (LH_shoot.leftHand)
        {
            anim.SetBool("IsPressed", true);
            windEffect.SetActive(true);
            //Debug.Log("LH_SHOOT is now " + LH_shoot.leftHand);
        }

        if (!LH_shoot.leftHand)
        {
            anim.SetBool("IsPressed", false);
            windEffect.SetActive(false);
            //Debug.Log("LH_SHOOT is now " + LH_shoot.leftHand);
        }
    }
}
