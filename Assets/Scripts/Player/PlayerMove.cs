using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    #region Variable
    //Managers
    [SerializeField]private GameManager m_gameManager;

    //Move + Attack
    public CharacterController2D controller;
    private float f_horizontalMove = 0f;
    public float runSpeed = 40;

    private bool b_isRigth;
    private bool b_isLeft;
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

    public const int maxHeal = 4;
    [SerializeField]private int f_currentHeal;

    private const int i_attackDamage = 1;

    private Vector2 m_sizeDetector = new Vector2(0.83f,1.40f);
    [SerializeField] private Vector2 m_crouchAttackpos;
    private Vector2 m_normalAttackpos;

    public Animator playerAnimator;
    public Collider2D[] playerColliders;
    public LayerMask enemyLayer;
    public Transform attackPoint;

    private float f_speedDir;
    public bool shopTrigger = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_normalAttackpos = attackPoint.localPosition;
        f_currentHeal = maxHeal;
        m_gameManager.UpdateHp(f_currentHeal);
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

            if (Input.GetButtonDown("Fire2") && f_currenTimeRoll >= f_cadenceRoll && f_horizontalMove != 0 && b_crouch == false && controller.m_Grounded == true)
            {
                StartCoroutine(IsRolling());
                f_currenTimeRoll = 0f;
            }
        }

        if(shopTrigger == true)
        {
            transform.position = transform.position;
        }
    }

    private void FixedUpdate()
    {
        //Detect Enemyes
        Collider2D[] detectRigthEnemy = Physics2D.OverlapBoxAll(controller.rigthDetector.position, m_sizeDetector, 0, enemyLayer);
        Collider2D[] detectLeftEnemy = Physics2D.OverlapBoxAll(controller.leftDetector.position, m_sizeDetector, 0, enemyLayer);

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
        if(b_roll == false)
        {
            const int force = 20;
            if (b_death == false)
            {
                FindObjectOfType<AudioManager>().Play("Hit");

                f_currentHeal -= dmg;
                if (f_currentHeal < 0)
                {
                    f_currentHeal = 0;
                }
                m_gameManager.UpdateHp(f_currentHeal);
                playerAnimator.SetTrigger("HitPlayer");
                StartCoroutine(TakingDamage());

                if (b_isRigth)
                {
                    controller.m_Rigidbody2D.AddForceAtPosition(new Vector2(1, 0.1f) * force, transform.localPosition, ForceMode2D.Impulse);
                }
                else if (b_isLeft)
                {
                    controller.m_Rigidbody2D.AddForceAtPosition(new Vector2(-1, 0.1f) * force, transform.localPosition, ForceMode2D.Impulse);
                }
            }
        }
    }

    private IEnumerator TakingDamage()
    {
        float time = 0.5f;
        b_hited = true;

        if (f_currentHeal <= 0)
        {
            runSpeed = 0f;
            playerAnimator.SetBool("Death", true);
            m_gameManager.blackBG.DOFade(1, 3f);
            b_death = true;
            time = 10f;
            yield return new WaitForSeconds(2f);
            this.gameObject.SetActive(false);
            
            //ReloadScene
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        yield return new WaitForSeconds(time);
        b_hited = false;
    }

    private IEnumerator IsRolling()
    {
        const int force = 1000;
        b_roll = true;
        playerAnimator.SetBool("Roll", true);
        
        if (f_horizontalMove >= 40)
        {
            controller.m_Rigidbody2D.AddForceAtPosition(Vector2.right * force, transform.localPosition);
        }
        else if(f_horizontalMove <= -40)
        {
            controller.m_Rigidbody2D.AddForceAtPosition(Vector2.left * force, transform.localPosition);
        }

        yield return new WaitForSeconds(0.4f);

        playerAnimator.SetBool("Roll", false);
        b_roll = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(controller.rigthDetector.position, m_sizeDetector);
        Gizmos.DrawWireCube(controller.leftDetector.position, m_sizeDetector);
        Gizmos.DrawSphere(attackPoint.position,f_attackRange);
    }
}