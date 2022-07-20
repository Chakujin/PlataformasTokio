using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Move + Attack
    public CharacterController2D controller;
    private float f_horizontalMove = 0f;
    public float runSpeed;

    [SerializeField] private bool b_isRigth;
    [SerializeField] private bool b_isLeft;
    private bool b_jump;
    private bool b_crouch;
    private bool b_roll;
    private float f_currenTime = 0;
    private float f_cadence = 0.5f;
    private float f_currenTimeRoll = 0;
    private float f_cadenceRoll = 1f;
    private Vector2 m_sizeDetector;

    public Animator playerAnimator;
    public Collider2D[] playerColliders;
    public LayerMask enemyLayer;
    public Transform rigthDetector;
    public Transform leftDetector;

    private float f_speedDir;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        f_currenTime += Time.deltaTime;
        f_currenTimeRoll += Time.deltaTime;

        f_horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        f_speedDir = Mathf.Abs(f_horizontalMove);
        playerAnimator.SetFloat("Speed", f_speedDir);
       
        //-----------------------------------------------------------------------
        //Inputs
        //-----------------------------------------------------------------------
        if (Input.GetButtonDown("Jump") && b_crouch == false && b_jump == false && b_roll == false)
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

        if (Input.GetButtonDown("Fire1") && f_currenTime >= f_cadence && b_jump == false && b_roll == false)
        {
            if(f_speedDir > 0.01f)
            {
                playerAnimator.SetTrigger("AttackMove");
                f_currenTime = 0f;
            }
            else if (f_speedDir < 0.01f)
            {
                playerAnimator.SetTrigger("AttackNoMove");
                f_currenTime = 0f;
            }
            Attack();
        }

        if (Input.GetButtonDown("Fire2") && f_currenTimeRoll >= f_cadenceRoll && b_jump == false && b_crouch == false)
        {
            StartCoroutine(IsRolling());
            f_currenTimeRoll = 0f;
            playerAnimator.SetBool("Roll",true);
            b_roll = true;
        }
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(f_horizontalMove * Time.fixedDeltaTime, b_crouch, b_jump);
        b_jump = false;

        //Detect Enemyes
        Collider2D[] detectRigthEnemy = Physics2D.OverlapBoxAll(rigthDetector.position, m_sizeDetector, 0, enemyLayer);
        Collider2D[] detectLeftEnemy = Physics2D.OverlapBoxAll(leftDetector.position, m_sizeDetector, 0, enemyLayer);

        foreach (Collider2D detectEnemyRigth in detectRigthEnemy)
        {
            b_isRigth = true;
            b_isLeft = false;
        }
        foreach (Collider2D detectEnemyLeft in detectLeftEnemy)
        {
            b_isRigth = false;
            b_isLeft = true;
        }
    }

    public void OnLanding()
    {
        playerAnimator.SetBool("IsJumping", false);
        b_jump = false;
    }

    public void IsCrouch(bool isCrouching)
    {
        playerAnimator.SetBool("IsCrouching", isCrouching);
    }

    public void Attack()
    {

    }

    public void TakeDamage(int dmg)
    {
        Debug.Log("damage");
    }

    private IEnumerator IsRolling()
    {
        //No recibe daño
        yield return new WaitForSeconds(0.4f);

        playerAnimator.SetBool("Roll", false);
        b_roll = false;
    }
}