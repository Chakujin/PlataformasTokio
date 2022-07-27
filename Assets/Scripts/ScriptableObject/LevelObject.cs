using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelObject : MonoBehaviour
{
    //Data
    private LevelData m_data;
    public Image image;
    public TextMeshProUGUI nameText;
    public int sceneLoad;
    public bool isUnloked;

    //Data to ItemManager
    public LevelData Data
    {
        set //Take values from data
        {
            m_data = value;
            image.sprite = m_data.sceneImage;
            nameText.text = m_data.sceneName;
            sceneLoad = m_data.sceneLoad;
            isUnloked = m_data.isUnloked;
        }
    }
}
