using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : EnemyClass
{
    
    [SerializeField] private Vector2 m_sizeAttack;
    public LayerMask playerLayer;
    private bool b_startAttack = false;

    public Transform detectedPoint;
    public Transform detectRigth;
    public Transform detectLeft;
    public Transform[] pointMove;
    public Transform scalePoint;

    private int i_currentPoint;
    private Vector2 v_moveDirection;

    [SerializeField] private Vector2 sizeDetectors;

    // Start is called before the first frame update
    void Start()
    {
        heal = maxHeal;
    }

    // Update is called once per frame
    void Update()
    {
        if (die == false)
        {
            //Move and Detect
            if (Vector2.Distance(transform.position, pointMove[i_currentPoint].transform.position) < 0.5 && b_startAttack == false) //Change Point Move
            {
                enemyAnim.SetBool("Move", false);
                i_currentPoint++;
                i_currentPoint %= pointMove.Length;
                StartCoroutine(StopMove());
            }
            else if (move == true && b_startAttack == false) // Move character
            {
                enemyAnim.SetBool("Move", true);
                v_moveDirection = transform.position + pointMove[i_currentPoint].position;
                transform.position = Vector2.MoveTowards(transform.position, pointMove[i_currentPoint].transform.position, Time.deltaTime * speed);

                if (v_moveDirection.x - transform.position.x < transform.position.x)
                {
                    enemySprite.flipX = true;
                    scalePoint.localScale = new Vector2(-1,1);
                    //Debug.Log("Izquierda");
                }
                else if (v_moveDirection.x - transform.position.x > transform.position.x)
                {
                    enemySprite.flipX = false;
                    scalePoint.localScale = new Vector2(1, 1);
                    //Debug.Log("Derecha");
                }
            }
        }
    }
    private void FixedUpdate()
    {
        Collider2D[] hitEnemyes = Physics2D.OverlapBoxAll(detectedPoint.position, m_sizeAttack, playerLayer);
        Collider2D[] detectRigthPlayer = Physics2D.OverlapBoxAll(detectRigth.position, sizeDetectors, 0, playerLayer);
        Collider2D[] detectLeftPlayer = Physics2D.OverlapBoxAll(detectLeft.position, sizeDetectors, 0, playerLayer);

        if (b_startAttack == false && die == false)
        {
            foreach (Collider2D playerRigth in detectRigthPlayer)
            {
                detectHitRigth = true;
                detectHitLeft = false;
                scalePoint.localScale = new Vector2(1, 1);

                //Detect Player Rigth
                foreach (Collider2D player in hitEnemyes)
                {
                    //Attack Player
                    if (b_startAttack == false)
                    {
                        StartCoroutine(startAttack(player));
                        b_startAttack = true;
                        enemySprite.flipX = false;
                        move = false;
                    }
                }
            }

            foreach (Collider2D playerLeft in detectLeftPlayer)
            {
                detectHitRigth = false;
                detectHitLeft = true;
                scalePoint.localScale = new Vector2(-1, 1);
                //Detect Player Left
                foreach (Collider2D player in hitEnemyes)
                {
                    //Attack Player
                    if (b_startAttack == false)
                    {
                        StartCoroutine(startAttack(player));
                        b_startAttack = true;
                        enemySprite.flipX = true;
                        move = false;
                    }
                }
            }
        }
    }

    private IEnumerator startAttack(Collider2D playerDetected)
    {
        enemyAnim.SetTrigger("Attack");
        enemyAnim.SetBool("Move", false);
        if (playerDetected.GetComponentInParent<PlayerMove>().b_roll == false)
        {
            playerDetected.GetComponentInParent<PlayerMove>().TakeDamage(1);
        }

        if (detectHitLeft == true)
        {
            enemySprite.flipX = true;
        }
        else if (detectHitRigth == true)
        {
            enemySprite.flipX = false;
        }

        yield return new WaitForSeconds(2f);
        b_startAttack = false;
        move = true;
    }

    private IEnumerator StopMove()
    {
        move = false;
        yield return new WaitForSeconds(1f);
        move = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(detectedPoint.position, m_sizeAttack);
        Gizmos.DrawWireCube(detectRigth.position, sizeDetectors);
        Gizmos.DrawWireCube(detectLeft.position, sizeDetectors);
    }
}