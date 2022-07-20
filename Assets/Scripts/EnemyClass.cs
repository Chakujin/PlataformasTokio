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

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
