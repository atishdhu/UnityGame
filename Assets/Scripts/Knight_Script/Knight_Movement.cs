﻿using UnityEngine;
using Cinemachine;

public class Knight_Movement : Knight_PhysicsObject
{
    public CinemachineBrain cam;
    public Animator camanimator;

    public ParticleSystem dust;

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        if (camanimator.enabled == true || cam.enabled ==true)  //stops player from moving left & right when cinemachine brain and animator are disabled.
        {
                move.x = Input.GetAxis("Horizontal");
        

            if (Input.GetButtonDown("Jump") && grounded)
            {
                Knight_SoundManager.PlaySound("Knight_Jump2");
                velocity.y = jumpTakeOffSpeed;
                createDust();
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }

            if (move.x > 0.01f)
            {
                if (spriteRenderer.flipX == true)
                {
                    spriteRenderer.flipX = false;
                    if (grounded == true)
                    {
                        createDust();
                    }
                }
            }
            else if (move.x < -0.01f)
            {
                if (spriteRenderer.flipX == false)
                {
                    spriteRenderer.flipX = true;
                    if (grounded == true)
                    {
                        createDust();
                    }
                }
            }

            animator.SetBool("grounded", grounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }
    }

        public void createDust()
    {
        dust.Play();
    }
}