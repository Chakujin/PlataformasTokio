using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelObject : MonoBehaviour
{
    //SCRIPTABLE OBJECT USE FOR MAIN MENU LEVEL SELECTION
    //Data
    private LevelData m_data;
    public Image image;
    public TextMeshProUGUI nameText;
    public int sceneLoad;
    public GameObject myButton;

    private GameData m_gameData;
    private bool b_isUnloked;

    //Data to ItemManager
    public LevelData Data
    {
        set //Take values from data
        {
            m_data = value;
            image.sprite = m_data.sceneImage;
            nameText.text = m_data.sceneName;
            sceneLoad = m_data.sceneLoad;
        }
    }
    private void Start()
    {
        m_gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();

        switch (sceneLoad) //No se como optimizar esto
        {
            case 1:
                b_isUnloked = m_gameData.level1;
                break;
            case 2:
                b_isUnloked = m_gameData.level2;
                break;
            case 3:
                b_isUnloked = m_gameData.level3;
                break;
            default:
                Debug.Log("This scene is not built or programmed");
                break;
        }

        if (b_isUnloked == false)
            myButton.SetActive(false);
    }
    //Button Load Scene
    public void ClickButton()
    {
        SceneManager.LoadScene(sceneLoad);
    }
}
