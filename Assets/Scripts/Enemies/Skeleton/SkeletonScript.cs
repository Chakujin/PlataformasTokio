using System.Collections;
using UnityEngine;

public class SkeletonScript : EnemyClass
{
    //Maquina de estados
    public MonoBehaviour NPCInicialState;
    public MonoBehaviour NPCMoveState;
    public MonoBehaviour NPCAttackState;

    private MonoBehaviour NPCActualState;

    //Var
    public bool roll = false;
    [SerializeField] private Vector2 m_sizeAttack;
    public LayerMask playerLayer;
    public bool b_startAttack = false;

    public Transform detectedPoint;
    public Transform detectRigth;
    public Transform detectLeft;
    public Transform scalePoint;

    [SerializeField] private Vector2 sizeDetectors;

    // Start is called before the first frame update
    void Start()
    {
        heal = maxHeal;

        //State Machine
        NPCMoveState.enabled = false;
        NPCAttackState.enabled = false;
        ChangeState(NPCInicialState);
    }

    private void FixedUpdate()
    {
        //Invisible Colliders 
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
                    if (b_startAttack == false && takingDmg == false)
                    {
                        ChangeState(NPCAttackState);
                        b_startAttack = true;
                        enemySprite.flipX = false;
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
                    if (b_startAttack == false && takingDmg == false)
                    {
                        ChangeState(NPCAttackState);
                        b_startAttack = true;
                        enemySprite.flipX = true;
                    }
                }
            }
        }
    }

    public void ChangeState(MonoBehaviour newState)
    {
        if (NPCActualState != null)
        {
            NPCActualState.enabled = false;
        }
        NPCActualState = newState;
        NPCActualState.enabled = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(detectedPoint.position, m_sizeAttack);
        Gizmos.DrawWireCube(detectRigth.position, sizeDetectors);
        Gizmos.DrawWireCube(detectLeft.position, sizeDetectors);
    }
}