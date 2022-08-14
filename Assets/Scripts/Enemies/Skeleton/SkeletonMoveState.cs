using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : MonoBehaviour
{
    //State Machine
    private SkeletonScript StateMachine;
    private MonoBehaviour NPCMoveState;

    //VAR
    public Transform[] pointMove;
    private Animator m_myAnim;

    public float speed;
    private int i_currentPoint;
    
    private Vector2 v_moveDirection;

    //Otras variables de clase para sus funcionalidades específicas
    void Awake()
    {
        StateMachine = GetComponent<SkeletonScript>();
        NPCMoveState = StateMachine.NPCMoveState;

        m_myAnim = StateMachine.enemyAnim;
    }

    // Update is called once per frame
    void Update()
    {
        if (StateMachine.die == false)
        {
            //Move and Detect
            if (Vector2.Distance(transform.position, pointMove[i_currentPoint].transform.position) < 0.5) //Change Point Move
            {
                m_myAnim.SetBool("Move", false);
                i_currentPoint++;
                i_currentPoint %= pointMove.Length;
                StartCoroutine(StopMove());
            }
            else if (StateMachine.move == true) // Move character
            {
                m_myAnim.SetBool("Move", true);
                v_moveDirection = transform.position + pointMove[i_currentPoint].position;
                transform.position = Vector2.MoveTowards(transform.position, pointMove[i_currentPoint].transform.position, Time.deltaTime * speed);

                if (v_moveDirection.x - transform.position.x < transform.position.x)
                {
                    StateMachine.enemySprite.flipX = true;
                    StateMachine.scalePoint.localScale = new Vector2(-1, 1);
                    //Debug.Log("Izquierda");
                }
                else if (v_moveDirection.x - transform.position.x > transform.position.x)
                {
                    StateMachine.enemySprite.flipX = false;
                    StateMachine.scalePoint.localScale = new Vector2(1, 1);
                    //Debug.Log("Derecha");
                }
            }
        }
    }

    private IEnumerator StopMove()
    {
        StateMachine.move = false;
        yield return new WaitForSeconds(1f);
        StateMachine.move = true;
    }
}
