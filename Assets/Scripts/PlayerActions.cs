using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    public CharacterController2D playerController;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    bool jump = false;

    bool attacking = false;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        bool wasAttacking = attacking;
        attacking = !(Time.time >= nextAttackTime);

        if(!attacking){
            if ( wasAttacking ) {
                playerController.UnfreezeAxisX();

                string animation = playerController.GetWhichAnimation();
                if (animation == "Bandit_Fall") {    
                    animator.SetBool("IsFalling", true);
                }
            }

            if (Input.GetKeyDown(KeyCode.F)){
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            } else {
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
                animator.SetFloat("SpeedX", Mathf.Abs(horizontalMove));

                if (Input.GetButtonDown("Jump")){
                    Jump();
                }   
            }
        }
    }

    void FixedUpdate ()
    {
        playerController.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }




    public void OnLanding ()
    {
        if( animator.GetCurrentAnimatorStateInfo(0).IsName("Bandit_Jump") ){
            animator.SetBool("IsJumping", false);
            if ( animator.GetFloat("SpeedX") < 0.01 ){
                animator.Play("Bandit_Idle");
            } else {
                animator.Play("Bandit_Run");
            }
        } else {
            animator.SetBool("IsFalling", false);
        }
            
    }

    public void OnFalling ()
    {   
        if ( !attacking ) {
            animator.SetBool("IsJumping", false);  
            animator.SetBool("IsFalling", true);
        }          
    }

    public void Jump () 
    {
        jump = true;
        animator.SetBool("IsJumping", true);
    }

    public void Attack () 
    {
        if ( !playerController.IsOnAir() ) {
            playerController.FreezeAxisX();
        }

        HandleForAttack();
        animator.SetTrigger("Attack");
    }

    public void HandleForAttack ()
    {
        animator.SetBool("IsJumping", false);  
        animator.SetBool("IsFalling", false);
    }

}
