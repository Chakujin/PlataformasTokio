using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : MonoBehaviour
{
    //State Machine
    private SkeletonScript StateMachine;
    private MonoBehaviour NPCMoveState;

    //VAR
    private Animator m_myAnim;
    private PlayerMove m_playerMove;
    private bool b_firtsTime = false;

    // Start is called before the first frame update
    void Start()
    {
        m_playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        StateMachine = GetComponent<SkeletonScript>();
        NPCMoveState = StateMachine.NPCMoveState;

        m_myAnim = StateMachine.enemyAnim;
        Debug.Log("Paso");
        StartCoroutine(startAttack());
        b_firtsTime = true;
    }

    private void OnEnable()
    {
        if(b_firtsTime == true)
        StartCoroutine(startAttack());
    }

    private IEnumerator startAttack()
    {
        m_myAnim.SetTrigger("Attack");
        m_myAnim.SetBool("Move", false);
        if (m_playerMove.b_roll == false)
        {
            m_playerMove.TakeDamage(1);
        }

        //Flip sprite
        if (StateMachine.detectHitLeft == true)
        {
            StateMachine.enemySprite.flipX = true;
        }
        else if (StateMachine.detectHitRigth == true)
        {
            StateMachine.enemySprite.flipX = false;
        }

        yield return new WaitForSeconds(2f);
        StateMachine.b_startAttack = false;
        StateMachine.ChangeState(NPCMoveState);
    }
}
