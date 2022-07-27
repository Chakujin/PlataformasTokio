using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelObject : MonoBehaviour
{
    //Data
    private LevelData m_data;

    public Image image;
    public TextMeshProUGUI nameText;

    //Data to ItemManager
    public LevelData Data
    {
        set //Take values from data
        {
            m_data = value;
            image.sprite = m_data.itemImage;
            nameText.text = m_data.itemName;
        }
    }
}
