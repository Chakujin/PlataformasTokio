using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //ARCHIVE UPDATED BY SAVESYSTEM
    //LEVEL UNLOCK
    public bool level1 = true;
    public bool level2;
    public bool level3;
    public bool level4;
    public bool level5;
    public bool level6;

    public float volumeMain;
    public bool gameStarted;

    public PlayerData(GameData manager)
    {
        //LEVELS
        level1 = manager.level1;
        level2 = manager.level2;
        level3 = manager.level3;
        level4 = manager.level4;
        level5 = manager.level5;
        level6 = manager.level6;

        gameStarted = manager.gameStarted;
        volumeMain = manager.MainVolume;
    }
}
