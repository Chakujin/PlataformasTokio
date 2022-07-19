using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Move + Attack
    public CharacterController2D controller;
    private float f_horizontalMove = 0f;
    public float runSpeed;

    [SerializeField]private bool b_jump;
    private bool b_crouch;
    private float f_currenTime = 0;

    private float f_cadence = 1.5f;

    public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        f_currenTime += Time.deltaTime;

        f_horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        playerAnimator.SetFloat("Speed", Mathf.Abs(f_horizontalMove));

        //Inputs

        if (Input.GetButtonDown("Jump") && b_crouch == false && b_jump == false)
        {
            b_jump = true;
            playerAnimator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            b_crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            b_crouch = false;
        }

        if (Input.GetButtonDown("Fire1") && f_currenTime >= f_cadence && b_jump == false)
        {
            if(f_horizontalMove >= 0.01f)
            {
                playerAnimator.SetTrigger("AttackMove");
                f_currenTime = 0f;
            }
            else if (f_horizontalMove < 0.01f)
            {
                playerAnimator.SetTrigger("AttackNoMove");
                f_currenTime = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(f_horizontalMove * Time.fixedDeltaTime, b_crouch, b_jump);
        b_jump = false;
    }

    public void OnLanding ()
    {
        playerAnimator.SetBool("IsJumping", false);
    }

    public void IsCrouch(bool isCrouching)
    {
        playerAnimator.SetBool("IsCrouching", isCrouching);
    }
}
