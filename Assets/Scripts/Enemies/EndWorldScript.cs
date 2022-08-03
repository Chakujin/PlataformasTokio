using UnityEngine;

public class EndWorldScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerMove>().TakeDamage(10);
        }
    }
}
