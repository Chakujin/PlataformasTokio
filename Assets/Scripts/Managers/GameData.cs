using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData inst;

    //IN GAME DATA CAN CHANGE AND NEED SAVE GAME TO UPDATE ARCHIVE

    //LEVEL UNLOCK
    public bool level1 = true;
    public bool level2;
    public bool level3;
    public bool level4;
    public bool level5;
    public bool level6;

    public float MainVolume = 1f;
    public bool gameStarted;

    //Start
    private void Awake()
    {
        if (GameData.inst == null)
        {
            GameData.inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Call this to save
    public void saveGame()
    {
        SaveSystem.saveGame(this);
    }

    //Call this to load
    public void loadGame()
    {
        PlayerData data = SaveSystem.loadGame();
        
        //LEVELS
        level1 = data.level1;
        level2 = data.level2;
        level3 = data.level3;
        level4 = data.level4;
        level5 = data.level5;
        level6 = data.level6;

        gameStarted = data.gameStarted;
        MainVolume = data.volumeMain;
    }
}
