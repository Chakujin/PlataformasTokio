using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int coins;
    public Image blackBG;
    private DiamondCount diamondCount;

    private void Start()
    {
        blackBG = GameObject.FindGameObjectWithTag("blackBG").GetComponent<Image>();
        diamondCount = GameObject.FindGameObjectWithTag("DiamondCount").GetComponent<DiamondCount>();

        UpdateDiamondCount();
    }

    public void UpdateDiamondCount()
    {
        diamondCount.updateDiamondCount(coins);
    }
}