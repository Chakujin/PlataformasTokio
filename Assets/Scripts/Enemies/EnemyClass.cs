using System.Collections;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public Animator enemyAnim;
    public Collider2D enemyColl;
    public int heal;
    public int maxHeal;

    public float speed;
    public bool die = false;
    public bool takingDmg = false;
    public bool move = true;
    public bool detectHitRigth = false;
    public bool detectHitLeft = false;
    public SpriteRenderer enemySprite;

    public void TakeDamage(int dmg)
    {
        FindObjectOfType<AudioManager>().Play("HitEnemy");
        StartCoroutine(DamageCor(dmg));
    }

    private IEnumerator DamageCor(int dmg)
    {
        takingDmg = true;
        float timeStop = 1f;
        move = false;
        heal -= dmg;
        enemyAnim.SetTrigger("Hited");

        if (heal <= 0)
        {
            FindObjectOfType<AudioManager>().Play("EnemyDie");
            timeStop = 10;
            die = true;
            speed = 0;
            enemyAnim.SetBool("Death", true);
            enemyColl.enabled = false;
            yield return new WaitForSeconds(3f);
            Destroy(this.gameObject);
        }
        yield return new WaitForSeconds(timeStop);
        move = true;
        takingDmg = false;
    }
}