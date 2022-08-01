using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ShopScript : MonoBehaviour
{
    private GameData m_gameData;
    private GameManager m_gameManager;

    // Start is called before the first frame update
    void Start()
    {
        m_gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        switch (SceneManager.GetActiveScene().buildIndex) //No se como optimizar esto
        {
            case 1:
                m_gameData.level1 = true;
                break;
            case 2:
                m_gameData.level2 = true;
                break;
            case 3:
                m_gameData.level3 = true;
                break;
            case 4:
                m_gameData.level4 = true;
                break;
            case 5:
                m_gameData.level5 = true;
                break;
            case 6:
                 m_gameData.level6 = true;
                break;
            default:
                Debug.Log("This scene is not built or programmed");
                break;
        }
        m_gameManager.blackBG.DOFade(1, 2f);
        yield return new WaitForSeconds(2f);
        m_gameData.saveGame();
        SceneManager.LoadScene(0);
    }
}
