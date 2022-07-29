using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : EnemyClass
{
    private bool b_startAttack = false;
    public Transform[] pointMove;

    private int i_currentPoint;
    private Vector2 v_moveDirection;

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
                }
                else if (v_moveDirection.x - transform.position.x > transform.position.x)
                {
                    enemySprite.flipX = false;
                }
            }
        }
    }

    private IEnumerator StopMove()
    {
        move = false;
        yield return new WaitForSeconds(1f);
        move = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && die == false)
        {
            StepSlime(collision.gameObject.GetComponent<PlayerMove>());
        }
    }

    private void StepSlime(PlayerMove myPlayer)
    {
        TakeDamage(1);
        myPlayer.TakeDamage(2);
    }
}