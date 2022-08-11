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

    // Start is called before the first frame update
    void Start()
    {
        m_playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        StateMachine = GetComponent<SkeletonScript>();
        NPCMoveState = StateMachine.NPCMoveState;

        m_myAnim = StateMachine.enemyAnim;

        startAttack(m_playerMove);
    }

    private IEnumerator startAttack(PlayerMove playerDetected)
    {
        m_myAnim.SetTrigger("Attack");
        m_myAnim.SetBool("Move", false);
        if (playerDetected.GetComponentInParent<PlayerMove>().b_roll == false)
        {
            playerDetected.GetComponentInParent<PlayerMove>().TakeDamage(1);
        }

        if (StateMachine.detectHitLeft == true)
        {
            StateMachine.enemySprite.flipX = true;
        }
        else if (StateMachine.detectHitRigth == true)
        {
            StateMachine.enemySprite.flipX = false;
        }

        yield return new WaitForSeconds(2f);
        StateMachine.ChangeState(NPCMoveState);
    }
}
