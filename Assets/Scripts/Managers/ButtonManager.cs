using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject selectLevelMenu;
    public GameObject optionsMenu;
    public GameData gameData;
    // Start is called before the first frame update
    void Awake()
    {
        if(gameData.gameStarted == false)
        {
            gameData.level1 = true;
            gameData.gameStarted = true;
            gameData.saveGame();
        }
        else
        {
            gameData.loadGame();
        }
    }

    private void Start()
    {
        selectLevelMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void SelectLevel()
    {
        selectLevelMenu.SetActive(true);
    }
    public void Options()
    {
        optionsMenu.SetActive(true);
    }

    public void Back()
    {
        optionsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
