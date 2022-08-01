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

    public float volumeMain;
    public bool gameStarted;

    public PlayerData(GameData manager)
    {
        //LEVELS
        level1 = manager.level1;
        level2 = manager.level2;
        level3 = manager.level3;

        gameStarted = manager.gameStarted;
        volumeMain = manager.MainVolume;
    }
}
