using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : EnemyClass
{
    //Attack + Detect
    private const float f_sizeAttack = 6;
    [SerializeField]private Vector2 m_sizeDetectors;
    public LayerMask playerLayer;

    public Transform detectPoint;
    public Transform detectRigth;
    public Transform detectLeft;
    public Transform spawnBullet;

    private bool b_startAttack;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        heal = maxHeal;
    }

    private void FixedUpdate()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(detectPoint.position, f_sizeAttack, playerLayer);
        Collider2D[] detectRigthPlayer = Physics2D.OverlapBoxAll(detectRigth.position, m_sizeDetectors, 0, playerLayer);
        Collider2D[] detectLeftPlayer = Physics2D.OverlapBoxAll(detectLeft.position, m_sizeDetectors, 0, playerLayer);

        if (b_startAttack == false && die == false)
        {
            foreach (Collider2D playerRigth in detectRigthPlayer)
            {
                detectHitRigth = true;
                detectHitLeft = false;
                spawnBullet.transform.localPosition = new Vector2(0.6f, spawnBullet.transform.localPosition.y);

                //Detect Player Rigth
                foreach (Collider2D player in hitPlayer)
                {
                    //Attack Player
                    if (b_startAttack == false)
                    {
                        StartCoroutine(StartAttack());
                        b_startAttack = true;
                        enemySprite.flipX = true;
                        move = false;
                    }
                }
            }

            foreach (Collider2D playerLeft in detectLeftPlayer)
            {
                detectHitRigth = false;
                detectHitLeft = true;
                spawnBullet.transform.localPosition = new Vector2(-0.6f, spawnBullet.transform.localPosition.y);
                //Detect Player Left
                foreach (Collider2D player in hitPlayer)
                {
                    //Attack Player
                    if (b_startAttack == false)
                    {
                        StartCoroutine(StartAttack());
                        b_startAttack = true;
                        enemySprite.flipX = false;
                        move = false;
                    }
                }
            }
        }
    }

    private IEnumerator StartAttack()
    {
        b_startAttack = true;
        enemyAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.7f);
        
        GameObject spawned = Instantiate(bullet, spawnBullet);
        if(detectHitLeft == true)
        {
            spawned.GetComponent<BulletScript>().speed = -4;
            spawned.GetComponent<BulletScript>().spriteRenderer.flipX = true;
        }
             
        yield return new WaitForSeconds(3f);
        b_startAttack = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(detectPoint.position, f_sizeAttack);
        Gizmos.DrawWireCube(detectRigth.position, m_sizeDetectors);
        Gizmos.DrawWireCube(detectLeft.position, m_sizeDetectors);
    }
}