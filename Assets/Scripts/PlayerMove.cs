using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Move + Attack
    public CharacterController2D controller;
    private float f_horizontalMove = 0f;
    public float runSpeed = 40;

    private bool b_jump;
    private bool b_crouch;
    public bool b_roll;
    private bool b_death =  false;
    private bool b_hited = false;

    private float f_currenTime = 0;
    private const float f_cadence = 0.5f;
    private float f_currenTimeRoll = 0;
    private const float f_cadenceRoll = 1f;
    [SerializeField]private float f_attackRange;

    public float maxHeal;
    private float f_currentHeal;

    private int i_attackDamage = 1;

    private Vector2 m_sizeDetector = new Vector2(0.83f,1.40f);
    [SerializeField] private Vector2 m_crouchAttackpos;
    private Vector2 m_normalAttackpos;

    public Animator playerAnimator;
    public Collider2D[] playerColliders;
    public LayerMask enemyLayer;
    public Transform rigthDetector;
    public Transform leftDetector;
    public Transform attackPoint;

    private float f_speedDir;

    // Start is called before the first frame update
    void Start()
    {
        m_normalAttackpos = attackPoint.localPosition;
        f_currentHeal = maxHeal;
    }

    // Update is called once per frame
    void Update()
    {
        f_currenTime += Time.deltaTime;
        f_currenTimeRoll += Time.deltaTime;

        if(b_hited == false)
        {
            f_horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }

        f_speedDir = Mathf.Abs(f_horizontalMove);
        playerAnimator.SetFloat("Speed", f_speedDir);

        playerAnimator.SetBool("isGrounded", controller.m_Grounded);

        if (b_hited == false)
        {
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

            if (Input.GetButtonDown("Fire1") && f_currenTime >= f_cadence && controller.m_Grounded == true && b_roll == false)
            {
                if (f_speedDir > 0.01f)
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
                playerAnimator.SetBool("Roll", true);
                b_roll = true;
            }
        }
    }

    private void FixedUpdate()
    {
        // Move our character
        controller.Move(f_horizontalMove * Time.fixedDeltaTime, b_crouch, b_jump);
        b_jump = false;
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
        if (b_crouch == true)
        {
            attackPoint.localPosition = m_crouchAttackpos;
        }
        else
        {
            attackPoint.localPosition = m_normalAttackpos;
        }

        Collider2D[] hitEnemyes = Physics2D.OverlapCircleAll(attackPoint.position, f_attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemyes)
        {
            enemy.GetComponent<EnemyClass>().TakeDamage(i_attackDamage);
        }
    }

    public void TakeDamage(int dmg)
    {
        if (b_death == false)
        {
            //FindObjectOfType<AudioManager>().Play("Hit");

            f_currentHeal -= dmg;
            playerAnimator.SetTrigger("HitPlayer");
            StartCoroutine(TakingDamage());

            //CameraPlayer.Instance.ShakeCamera(3f, 0.25f); // ShakeCam
        }
    }

    private IEnumerator TakingDamage()
    {
        float time = 1f;
        b_hited = true;

        foreach(Collider2D coll in playerColliders)
        {
            coll.enabled = false;
        }

        if (f_currentHeal <= 0)
        {
            playerAnimator.SetBool("Death", true);

            //StartCoroutine(diePlayer());
            b_death = true;
            time = 10f;

            yield return new WaitForSeconds(2f);
            this.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(time);

        b_hited = false;

        foreach (Collider2D coll in playerColliders)
        {
            coll.enabled = true;
        }
    }

    private IEnumerator IsRolling()
    {
        //No recibe daño
        yield return new WaitForSeconds(0.4f);

        playerAnimator.SetBool("Roll", false);
        b_roll = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(rigthDetector.position, m_sizeDetector);
        Gizmos.DrawWireCube(leftDetector.position, m_sizeDetector);
        Gizmos.DrawSphere(attackPoint.position,f_attackRange);
    }
}