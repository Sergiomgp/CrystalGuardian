﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    private PlayerStats player;
    private PlayerSystems p_Systems;
    public Camera cam;
    public GameObject[] projectile;
    public Transform LHFirePoint, RHFirePoint;
    public Slider _chargebarSlider;
    public float chargeTime;
    private float shootCooldown = .4f;
    [SerializeField] private bool onCooldown = false;

    public float projectileSpeed = 100f;

    [SerializeField] public bool leftHand = false;
    [SerializeField] public bool rightHand = false;
    [SerializeField] public bool isCharging = false;

    //test variables
    public float newChargeTime;


    private Vector3 destination;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>(); 
        p_Systems = GameObject.FindGameObjectWithTag("GameSystems").GetComponent<PlayerSystems>();
    }
    // Update is called once per frame
    void Update()
    {
        isCharging = false;
        if (player.hasMana)
        {
            if (Input.GetMouseButton(0) && !rightHand && !onCooldown)
            {
                if (isCharging == true || rightHand == true) { Debug.Log("Already charging"); return; }

                isCharging = true;
                leftHand = true;
                if (leftHand)
                {
                    ChargeAttack();
                }
                //Debug.Log("right hand is" + rightHand);
            }
            if (Input.GetMouseButtonUp(0) && !rightHand && !onCooldown)
            {
                if (leftHand)
                {
                    LaunchAttack();
                    leftHand = false;
                }
            }
            if (Input.GetMouseButton(1) && !leftHand && !onCooldown)
            {
                if (isCharging == true || leftHand == true) { Debug.Log("Already charging"); return; }

                isCharging = true;
                rightHand = true;
                if (rightHand)
                {
                    ChargeAttack();
                }
                //Debug.Log("left hand is" + leftHand);
            }
            if (Input.GetMouseButtonUp(1) && !leftHand && !onCooldown)
            {
                if (rightHand)
                {
                    LaunchAttack();
                    rightHand = false;
                }
            }
        }
    }

    #region Charge Attack 
    private void ChargeAttack()
    {
        chargeTime += Time.deltaTime;
        _chargebarSlider.value += Time.deltaTime;
        newChargeTime = chargeTime;
    }

    private void LaunchAttack()
    {
        onCooldown = true;
        //Debug.Log("PROJECTILY SHOOT");
        WeakAttack();
        MidAttack();
        StrongAttack();
        isCharging = false;
        StartCoroutine(ShootingCooldown());
    }

    private void WeakAttack()
    {
        if (chargeTime <= 1)
        {
            ShootProjectile(0);
            p_Systems.ConsumeMana(5);
            //Debug.Log("Weak Attack");
            chargeTime = 0;
            _chargebarSlider.value = chargeTime;
        }
    }

    private void MidAttack()
    {
        if (chargeTime > 1 && chargeTime < 2)
        {
            //insert shootprojectile
            ShootProjectile(0);
            p_Systems.ConsumeMana(15);
            //Debug.Log("Mid Attack");
            chargeTime = 0;
            _chargebarSlider.value = chargeTime;
        }
    }

    private void StrongAttack()
    {
        if (chargeTime >= 2)
        {
            p_Systems.ConsumeMana(30);
            //Debug.Log("Strong Attack");
            chargeTime = 0;
            _chargebarSlider.value = chargeTime;
            ShootProjectile(0);
        }
    }
    #endregion

    #region ShootProjectile
    void ShootProjectile(int index)
    {
        //creates a point that is used as the destination of the projectiles
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
            destination = ray.GetPoint(1000);

        // instantiates the correct prefab according to the index
        if (leftHand)
        {
            InstantiateProjectile(LHFirePoint, 0);
            leftHand = false;
        }
        if (rightHand)
        {
            //the value one refers to the projectile index to be spawned
            InstantiateProjectile(RHFirePoint, 1);
            rightHand = false;
        }
    }

    //instantiates the projectile with the correct index, and launches it towards the ray destination
    void InstantiateProjectile(Transform firePoint, int index)
    {
        var projectilObj = Instantiate(projectile[index], firePoint.position, Quaternion.identity) as GameObject;
        projectilObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
    }
    #endregion

    // cooldown so player doesnt spam attack
    private IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(shootCooldown);
        onCooldown = false;
    }
}
