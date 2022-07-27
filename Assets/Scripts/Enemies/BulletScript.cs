using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private const float f_timeDelete = 4f;
    private float f_currenTime = 0;
    public float speed = 4;
    public SpriteRenderer spriteRenderer;

    // Update is called once per frame
    void Update()
    {
        f_currenTime =+ Time.deltaTime;
        if(f_currenTime >= f_timeDelete)
        {
            Destroy(gameObject);
        }
        
        transform.localPosition = new Vector2(transform.localPosition.x + speed * Time.deltaTime,transform.localPosition.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerMove>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
