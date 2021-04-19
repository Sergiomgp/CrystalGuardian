﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Basic player controller script
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Slider _staminaSlider;
    public float currentStamina;
    public float regenDelay = .5f;
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public float speed = 18f;
    public float speedMultiplier = 1.5f;
    public float sprintSpeed;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;

    Vector3 velocity;
    public bool isGrounded;

    private void Start()
    {
        currentStamina = _staminaSlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        Vector3 move = Movement();
        Sprint(move);
        Jump();
    }

    private Vector3 Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //moves the player in the direction in which he is facing
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        return move;
    }

    private void GroundCheck()
    {
        //checks for ground collision
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void Jump()
    {
        //checks if the player is touching the ground and if he is he can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void Sprint(Vector3 move)
    {
        //checks for left shift hold, if true, gives a speedboost to the player movement
        if (currentStamina >= _staminaSlider.minValue + 1)
        {
            if (Input.GetKey("left shift"))
            {
                sprintSpeed = speed * speedMultiplier + speed;
                controller.Move(move * sprintSpeed * Time.deltaTime);
                Debug.Log("The new speed is " + sprintSpeed);
                _staminaSlider.value -= 20 * Time.deltaTime;
                currentStamina = _staminaSlider.value;

                if (regen != null)
                {
                    StopCoroutine(regen);
                }
                regen = StartCoroutine(StaminaRegen());
            }
        }
        //Debug.Log(currentStamina); logs tue current stamina value
    }

    //regens player´s stamina
    private IEnumerator StaminaRegen()
    {

        yield return new WaitForSeconds(regenDelay);

        while (currentStamina <= _staminaSlider.maxValue)
        {
            currentStamina += _staminaSlider.maxValue / 250;
            _staminaSlider.value = currentStamina;

            yield return regenTick;
        }
        regen = null;
    }
}
