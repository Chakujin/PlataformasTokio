using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Move + Attack
    public CharacterController2D controller;
    private float f_horizontalMove = 0f;
    public float runSpeed;

    private bool b_jump;
    private bool b_crouch;

    public Animator playerAnimator;

    [SerializeField] private bool b_isRigth;
    [SerializeField] private bool b_isLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        f_horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        playerAnimator.SetFloat("Speed", Mathf.Abs(f_horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            b_jump = true;
            playerAnimator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            b_crouch = true;
        }
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(f_horizontalMove * Time.fixedDeltaTime, false, b_jump);
        b_jump = false;
    }

    public void OnLanding()
    {
        playerAnimator.SetBool("IsJumping", false);
    }
}
