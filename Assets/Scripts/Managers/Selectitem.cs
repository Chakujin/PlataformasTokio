using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Selectitem : MonoBehaviour
{
    public Transform itemConstainer;
    
    //Data
    public GameObject LevelItem;
    private LevelData[] m_levelData;

    // Start is called before the first frame update

    private void Awake()
    {

    }

    private void OnEnable()
    {
        for (int i = 0; i < m_levelData.Length; i++)
        {
            GameObject go = Instantiate(LevelItem, itemConstainer); //instanciate items
            LevelObject objectData = go.GetComponent<LevelObject>();
            objectData.Data = m_levelData[i];
        }
    }
}
