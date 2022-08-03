using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //UI
    public Image blackBG;

    //Coins
    public static int coins;
    private DiamondCount diamondCount;

    //HeartUI
    public Sprite[] heartSprites;
    [SerializeField]private Image m_heartHP;

    private void Start()
    {
        blackBG = GameObject.FindGameObjectWithTag("blackBG").GetComponent<Image>();
        diamondCount = GameObject.FindGameObjectWithTag("DiamondCount").GetComponent<DiamondCount>();
        m_heartHP = GameObject.FindGameObjectWithTag("HeartUi").GetComponent<Image>();

        UpdateDiamondCount();
    }

    public void UpdateDiamondCount()
    {
        coins++;
        diamondCount.updateDiamondCount(coins);
    }

    public void UpdateHp(int num)
    {
        m_heartHP.sprite = heartSprites[num];
    }
}