using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public Animator enemyAnim;
    public int heal;
    public int maxHeal;

    public float speed;
    public bool die = false;
    public bool move = true;
    public bool detectHitRigth = false;
    public bool detectHitLeft = false;
    public SpriteRenderer enemySprite;

    public void TakeDamage(int dmg)
    {
        Debug.Log("EnemyHit");
        heal -= dmg;
        enemyAnim.SetTrigger("Hited");

        if(heal <= 0)
        {
            die = true;
            speed = 0;
            enemyAnim.SetBool("Death", true);
        }
    }
}
