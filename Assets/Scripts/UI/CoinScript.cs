using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private GameManager m_gameManager;
    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            m_gameManager.coins++;
            m_gameManager.UpdateDiamondCount();
            Destroy(gameObject);
        }
    }
}