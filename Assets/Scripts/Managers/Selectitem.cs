using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Selectitem : MonoBehaviour
{
    public Transform itemConstainer;
    private List<GameObject> m_items = new List<GameObject>();
    //Data
    public GameObject LevelItem;
    [SerializeField]private LevelData[] m_levelData;

    // Start is called before the first frame update

    private void Awake()
    {

    }

    public void LoadObjects()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        for (int i = 0; i < m_levelData.Length; i++)
        {
            GameObject go = Instantiate(LevelItem, itemConstainer); //instanciate items
            LevelObject objectData = go.GetComponent<LevelObject>(); //Get script
            objectData.Data = m_levelData[i]; // Add Data LevelObject Data
            //------------------------------------------------------------------
            //---------------------------ANIMATIONS INSTANCE--------------------
            //------------------------------------------------------------------
            m_items.Add(go);
            go.transform.DOScale(0, 0f); //Reset sclae
            go.transform.DOScale(1, 0.75f).SetEase(Ease.OutElastic);//Animation
            yield return new WaitForSeconds(1f);//Delay spawn next object
        }
    }
    public void ResetList()
    {
        foreach(GameObject item in m_items)
        {
            Destroy(item);
        }
    }
}
